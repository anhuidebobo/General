using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services
{
    public interface ISysUserRoleService
    {
        /// <summary>
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        List<Entities.SysUserRole> GetAll();

        void removeCache();
    }
}
