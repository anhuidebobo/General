using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Entities;

namespace General.Framework.Security.Admin
{
    public class AuthenticationService : IAuthenticationService
    {
        public SysUser GetCurrentUser()
        {
            return new SysUser { Id = Guid.NewGuid(), Name = "bobo" };
        }
    }
}
