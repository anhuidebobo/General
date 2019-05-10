using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace General.Entities
{

    /// <summary>
    /// 类名称：SysUserLoginLog
    /// 命名空间：General.Entities
    /// 类功能：登录日志
    /// </summary>
    /// 创建者：libb
    /// 创建日期：2019/5/10 9:52
    /// 修改者：
    /// 修改时间：
    /// -----------------------------------------------------------
    [Table("SysUserLoginLog")]
    public class SysUserLoginLog
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string IpAddress { get; set; }

        public DateTime LoginTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        [ForeignKey("UserId")]
        public virtual SysUser SysUser { get; set; }
    }


}
