using General.Core;
using General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services.SysUser
{
    public interface ISysUserService
    {
        /// <summary>
        /// Valids the user.
        /// </summary>
        /// <param name="account">The account</param>
        /// <param name="password">The password</param>
        /// <param name="r">The r</param>
        /// <returns>
        /// ValueTuple&lt;System.Boolean, System.String, Entities.SysUser&gt;
        /// </returns>
        /// 创建者：libb
        /// 创建日期：2019/5/13 16:00
        /// 修改者：
        /// 修改时间：
        /// -----------------------------------------------------------
        //ValueTuple<bool, string, Entities.SysUser> ValidUser(string account, string password, string r);
        (bool Status, string Message,string Token, Entities.SysUser User) ValidUser(string account, string password, string r);

        Entities.SysUser GetByAccount(string account);

        Entities.SysUser GetLogged(string token);

        IPagedList<Entities.SysUser> searchUser(SysUserSearchArg arg, int page, int size);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Entities.SysUser getById(Guid id);

        /// <summary>
        /// 新增，插入
        /// </summary>
        /// <param name="model"></param>
        void insertSysUser(Entities.SysUser model);

        /// <summary>
        /// 更新修改
        /// </summary>
        /// <param name="model"></param>
        void updateSysUser(Entities.SysUser model);

        /// <summary>
        /// 重置密码。默认重置成账号一样
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        void resetPassword(Guid id, Guid modifer);

        /// <summary>
        /// 验证账号是否已经存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        bool existAccount(string account);


        /// <summary>
        /// 启用禁用账号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enabled"></param>
        /// <param name="modifer"></param>
        void enabled(Guid id, bool enabled, Guid modifer);

        /// <summary>
        /// 登录锁与解锁
        /// </summary>
        /// <param name="id"></param>
        /// <param name="ulock"></param>
        /// <param name="modifer"></param>
        void loginLock(Guid id, bool ulock, Guid modifer);

        /// <summary>
        /// 删除用户。无法删除超级用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="modifer"></param>
        void deleteUser(Guid id, Guid modifer);

        /// <summary>
        /// 添加用户头像
        /// </summary>
        /// <param name="id"></param>
        /// <param name="avatar"></param>
        void addAvatar(Guid id, byte[] avatar);

        /// <summary>
        /// 用户自己修改密码
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        void changePassword(Guid id, string password);

        /// <summary>
        /// 设置用户最后多动时间
        /// </summary>
        /// <param name="id"></param>
        void lastActivityTime(Guid id);
    }
}
