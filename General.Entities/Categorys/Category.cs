using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Entities
{
    /// <summary>
    /// 类名称：Category
    /// 命名空间：General.Entities.Categorys
    /// 类功能：菜单表
    /// </summary>
    /// 创建者：libb
    /// 创建日期：2019/5/10 9:46
    /// 修改者：
    /// 修改时间：
    /// -----------------------------------------------------------
    [Table("Category")]
    public class Category
    {
        public Category()
        {
            SysPermissions = new HashSet<SysPermission>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsMenu { get; set; }

        public string SysResource { get; set; }

        public string ResourceID { get; set; }

        public string FatherResource { get; set; }

        public string FatherID { get; set; }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string RouteName { get; set; }

        public string CssClass { get; set; }

        public int Sort { get; set; }

        public bool IsDisabled { get; set; }

        public virtual ICollection<SysPermission> SysPermissions { get; set; }

    }
}
