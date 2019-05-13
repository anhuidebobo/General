using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace General.Framework.Security.Admin
{
    public class AdminAuthService : IAdminAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        public AdminAuthService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public SysUser GetCurrentUser()
        {
            return new SysUser { Id = Guid.NewGuid(), Name = "bobo" };
        }

        public void SingIn(string token, string name)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity("General");
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid,token));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name,name));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            _httpContextAccessor.HttpContext.SignInAsync("General", claimsPrincipal);
        }
    }
}
