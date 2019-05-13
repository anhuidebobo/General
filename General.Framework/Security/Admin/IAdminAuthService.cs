using General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Security.Admin
{
    public interface IAdminAuthService
    {
        SysUser GetCurrentUser();

        void SingIn(string token, string name);
    }
}
