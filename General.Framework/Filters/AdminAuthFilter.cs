using General.Core;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class AdminAuthFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            var _adminAuthService = EngineContext.Current.Resolve<IAdminAuthService>();
            var user = _adminAuthService.GetCurrentUser();
            if (user == null || !user.Enabled)
            {
                context.Result = new RedirectToRouteResult("adminLogin", new { returnUrl = context.HttpContext.Request.Path });
            }
        }
    }
}
