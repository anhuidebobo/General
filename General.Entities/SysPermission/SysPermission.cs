using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{

    /// <summary>
    /// 类名称：SysPermission
    /// 命名空间：General.Entities
    /// 类功能：角色权限
    /// </summary>
    /// 创建者：libb
    /// 创建日期：2019/5/10 9:47
    /// 修改者：
    /// 修改时间：
    /// -----------------------------------------------------------
    [Table("SysPermission")]
    public partial class SysPermission
    {
        public Guid Id { get; set; }

        public int CategoryId { get; set; }

        public Guid RoleId { get; set; }

        public Guid Creator { get; set; }

        public DateTime CreationTime { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("RoleId")]
        public virtual SysRole SysRole { get; set; }

        [ForeignKey("Creator")]
        public virtual SysUser SysUser { get; set; }
    }
}
