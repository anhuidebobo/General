using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services
{
    public interface ISysPermissionService 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Entities.SysPermission> GetAll();

        void removeCache();

        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        List<Entities.SysPermission> getByRoleId(Guid id);

        /// <summary>
        /// 保存角色权限数据
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="categoryIds"></param>
        /// <param name="creator"></param>
        void saveRolePermission(Guid roleId, List<Guid> categoryIds, Guid creator);
    }
}
