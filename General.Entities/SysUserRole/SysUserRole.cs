using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{

    /// <summary>
    /// 类名称：SysUserRole
    /// 命名空间：General.Entities
    /// 类功能：用户角色
    /// </summary>
    /// 创建者：libb
    /// 创建日期：2019/5/10 9:52
    /// 修改者：
    /// 修改时间：
    /// -----------------------------------------------------------
    [Table("SysUserRole")]
    public partial class SysUserRole
    {
        public Guid Id { get; set; }

        
        public Guid RoleId { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("RoleId")]
        public virtual SysRole SysRole { get; set; }

        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }
}
