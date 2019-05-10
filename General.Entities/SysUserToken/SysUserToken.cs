using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{

    /// <summary>
    /// 类名称：SysUserToken
    /// 命名空间：General.Entities
    /// 类功能：登录Token
    /// </summary>
    /// 创建者：libb
    /// 创建日期：2019/5/10 9:52
    /// 修改者：
    /// 修改时间：
    /// -----------------------------------------------------------
    [Serializable]
    [Table("SysUserToken")]
    public partial class SysUserToken
    {
        public Guid Id { get; set; }

        public Guid SysUserId { get; set; }

        public DateTime ExpireTime { get; set; }

        [ForeignKey("SysUserId")]
        public virtual SysUser SysUser { get; set; }
    }
}
