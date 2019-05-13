using General.Core;
using General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services.SysUser
{
    public class SysUserService : ISysUserService
    {
        private IRepository<Entities.SysUser> _repository;
        public SysUserService(IRepository<Entities.SysUser> repository)
        {
            this._repository = repository;
        }

        public (bool, string, string, Entities.SysUser) ValidUser(string account, string password, string r)
        {
            return (true, "登录成功", "aaaa11111", new Entities.SysUser { Id = Guid.NewGuid(), Name = "bobo" });
        }
    }
}
