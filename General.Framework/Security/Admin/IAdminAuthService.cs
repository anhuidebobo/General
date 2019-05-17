using General.Entities;
using Microsoft.AspNetCore.Mvc.Filters;
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

        List<Category> GetMyCategories();

        void SingIn(string token, string name);

        void SignOut();

        bool authorize(ActionExecutingContext context);

        bool authorize(string routeName);
    }
}
