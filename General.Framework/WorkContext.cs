using General.Entities;
using General.Framework.Infrastructure;
using General.Framework.Security.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework
{
    public class WorkContext : IWorkContext
    {
        private IAdminAuthService _authenticationService;
        public WorkContext(IAdminAuthService AuthenticationService)
        {
            this._authenticationService = AuthenticationService;
        }
        public SysUser CurrentUser
        {
            get
            {
                return _authenticationService.GetCurrentUser();
            }
        }
    }
}
