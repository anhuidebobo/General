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
    }
}
