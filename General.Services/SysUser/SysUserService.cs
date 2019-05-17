using General.Core;
using General.Core.Librs;
using General.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService
    {
        private IMemoryCache _memoryCache;
        private const string MODEL_KEY = "General.services.user_{0}";

        private IRepository<Entities.SysUser> _repository;
        private IRepository<Entities.SysUserToken> _sysUserTokenRepository;
        public SysUserService(IRepository<Entities.SysUser> repository, IMemoryCache memoryCache,
             IRepository<Entities.SysUserToken> sysUserTokenRepository)
        {
            this._repository = repository;
            this._memoryCache = memoryCache;
            this._sysUserTokenRepository = sysUserTokenRepository;
        }

        public Entities.SysUser GetByAccount(string account)
        {
            return this._repository.Table.FirstOrDefault(o => o.Account == account && !o.IsDeleted);
        }

        public Entities.SysUser GetLogged(string token)
        {
            Entities.SysUserToken userToken = null;
            Entities.SysUser sysUser = null;
            _memoryCache.TryGetValue<Entities.SysUserToken>(token, out userToken);
            if (userToken != null)
                _memoryCache.TryGetValue(String.Format(MODEL_KEY, userToken.SysUserId), out sysUser);
            if (sysUser != null)
                return sysUser;
            Guid tokenId = Guid.Empty;
            if (Guid.TryParse(token, out tokenId))
            {
                var tokenItem = _sysUserTokenRepository.Table.Include(x => x.SysUser)
                    .FirstOrDefault(o => o.Id == tokenId);
                if (tokenItem != null)
                {
                    _memoryCache.Set(token, tokenItem, DateTimeOffset.Now.AddHours(4));
                    //缓存
                    _memoryCache.Set(String.Format(MODEL_KEY, tokenItem.SysUserId), tokenItem.SysUser
                        , DateTimeOffset.Now.AddHours(4));
                    return tokenItem.SysUser;
                }
            }
            return null;
        }

        public (bool Status, string Message, string Token, Entities.SysUser User) ValidUser(string account, string password, string r)
        {
            var user = GetByAccount(account);
            if (user == null)
            {
                return (false, "用户名或密码错误！", null, null);
            }
            //被冻结
            if (!user.Enabled)
                return (false, "用户已被冻结！", null, null);

            //被锁住的情况
            if (user.LoginLock)
            {
                if (user.AllowLoginTime > DateTime.Now)
                {
                    return (false, "账号已被锁定,剩余" + ((int)(user.AllowLoginTime - DateTime.Now).Value.TotalMinutes + 1) + "分钟", null, null);
                }
            }


            var md5Password = EncryptorHelper.GetMD5(user.Password + r);
            //登录成功
            if (password.Equals(md5Password, StringComparison.InvariantCultureIgnoreCase))
            {
                user.LoginLock = false;
                user.LoginFailedNum = 0;
                user.LastLoginTime = DateTime.Now;
                user.LastIpAddress = "";

                //登录日志
                user.SysUserLoginLogs.Add(new SysUserLoginLog
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登录：成功",
                    UserId = user.Id
                });
                var userToken = new Entities.SysUserToken
                {
                    Id = Guid.NewGuid(),
                    SysUserId = user.Id,
                    ExpireTime = DateTime.Now.AddDays(15)
                };
                user.SysUserTokens.Add(userToken);

                _repository.DbContext.SaveChanges();

                return (true, "登录成功", userToken.Id.ToString(), user);
            }
            else
            {
                //登录日志
                user.SysUserLoginLogs.Add(new SysUserLoginLog
                {
                    Id = Guid.NewGuid(),
                    IpAddress = "",
                    LoginTime = DateTime.Now,
                    Message = "登录：密码错误！",
                    UserId = user.Id
                });
                user.LoginFailedNum++;
                if (user.LoginFailedNum > 5)
                {
                    user.LoginLock = true;
                    user.AllowLoginTime = DateTime.Now.AddHours(2);
                }
                _repository.DbContext.SaveChanges();
            }

            return (false, "用户名或密码错误！", null, null);
        }

        /// <summary>
        /// 搜索数据
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public IPagedList<Entities.SysUser> searchUser(SysUserSearchArg arg, int page, int size)
        {
            var query = _repository.Table.Where(o => !o.IsDeleted);
            if (arg != null)
            {
                if (!String.IsNullOrEmpty(arg.q))
                    query = query.Where(o => o.Account.Contains(arg.q) || o.MobilePhone.Contains(arg.q) || o.Email.Contains(arg.q) || o.Name.Contains(arg.q));
                if (arg.enabled.HasValue)
                    query = query.Where(o => o.Enabled == arg.enabled);
                if (arg.unlock.HasValue)
                    query = query.Where(o => o.LoginLock == arg.unlock);
                if (arg.roleId.HasValue)
                    query = query.Where(o => o.SysUserRoles.Any(r => r.RoleId == arg.roleId));
            }
            query = query.OrderBy(o => o.Account).ThenBy(o => o.Name).ThenByDescending(o => o.CreationTime);
            return new PagedList<Entities.SysUser>(query, page, size);
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Entities.SysUser getById(Guid id)
        {
            return _repository.GetEntityById(id);
        }

        /// <summary>
        /// 新增，插入
        /// </summary>
        /// <param name="model"></param>
        public void insertSysUser(Entities.SysUser model)
        {
            if (existAccount(model.Account))
                return;
            _repository.Insert(model);
        }

        /// <summary>
        /// 更新修改
        /// </summary>
        /// <param name="model"></param>
        void updateSysUser(Entities.SysUser model)
        {
            _repository.DbContext.Entry(model).State = EntityState.Unchanged;
            _repository.DbContext.Entry(model).Property("Name").IsModified = true;
            _repository.DbContext.Entry(model).Property("Email").IsModified = true;
            _repository.DbContext.Entry(model).Property("MobilePhone").IsModified = true;
            _repository.DbContext.Entry(model).Property("Sex").IsModified = true;
            _repository.DbContext.SaveChanges();
        }

        /// <summary>
        /// 重置密码。默认重置成账号一样
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        public void resetPassword(Guid id, Guid modifer)
        {

        }

        /// <summary>
        /// 验证账号是否已经存在
        /// </summary>
        /// <param name="id"></param>
        /// <param name="account"></param>
        /// <returns></returns>
        public bool existAccount(string account)
        {
            return _repository.Table.Any(o => o.Account == account && !o.IsDeleted);
        }

        void ISysUserService.updateSysUser(Entities.SysUser model)
        {
            _repository.Update(model);
        }

        public void enabled(Guid id, bool enabled, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void loginLock(Guid id, bool ulock, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void deleteUser(Guid id, Guid modifer)
        {
            throw new NotImplementedException();
        }

        public void addAvatar(Guid id, byte[] avatar)
        {
            throw new NotImplementedException();
        }

        public void changePassword(Guid id, string password)
        {
            throw new NotImplementedException();
        }

        public void lastActivityTime(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 移除缓存用户
        /// </summary>
        /// <param name="userId"></param>
        private void removeCacheUser(Guid userId)
        {
            _memoryCache.Remove(String.Format(MODEL_KEY, userId));
        }




    }
}
