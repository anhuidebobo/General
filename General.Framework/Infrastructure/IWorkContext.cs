using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Infrastructure
{
    public interface IWorkContext
    {

        /// <summary>
        /// 当前登录用户
        /// </summary>
        /// 创建者：libb
        /// 创建日期：2019/5/15 9:38
        /// 修改者：
        /// 修改时间：
        /// -----------------------------------------------------------
        Entities.SysUser CurrentUser { get; }

        /// <summary>
        /// 当前登录用户的菜单
        /// </summary>
        List<Entities.Category> Categories { get; }
    }
}
