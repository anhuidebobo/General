using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using General.Entities;
using General.Services;
using General.Services.SysUser;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace General.Framework.Security.Admin
{
    public class AdminAuthService : IAdminAuthService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private ISysUserService _sysUserService;
        private ICategoryService _categoryService;
        private ISysUserRoleService _sysUserRoleService;
        private ISysPermissionService _sysPermissionServices;
        public AdminAuthService(IHttpContextAccessor httpContextAccessor,
            ISysUserService sysUserService,
            ICategoryService categoryService,
            ISysPermissionService sysPermissionServices,
            ISysUserRoleService sysUserRoleService)
        {
            this._httpContextAccessor = httpContextAccessor;
            this._sysUserService = sysUserService;
            this._categoryService = categoryService;
            this._sysUserRoleService = sysUserRoleService;
            this._sysPermissionServices = sysPermissionServices;
        }

        public SysUser GetCurrentUser()
        {
            var result = _httpContextAccessor.HttpContext.AuthenticateAsync(CookieAdminAuthInfo.AuthenticationScheme).Result;
            if (result.Principal == null)
                return null;
            var token = result.Principal.FindFirstValue(ClaimTypes.Sid);
            return _sysUserService.GetLogged(token ?? "");
        }

        public void SingIn(string token, string name)
        {
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Sid, token));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, name));
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            _httpContextAccessor.HttpContext.SignInAsync(CookieAdminAuthInfo.AuthenticationScheme, claimsPrincipal);
        }

        public void SignOut()
        {
            _httpContextAccessor.HttpContext.SignOutAsync(CookieAdminAuthInfo.AuthenticationScheme);
        }

        /// <summary>
        /// 获取我的权限数据
        /// </summary>
        /// <returns></returns>
        public List<Category> GetMyCategories()
        {
            var user = GetCurrentUser();
            return getMyCategories(user);
        }

        /// <summary>
        /// 私有方法，获取当前用户的方法数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private List<Entities.Category> getMyCategories(Entities.SysUser user)
        {
            var list = _categoryService.GetAll();
            if (user == null) return null;
            if (user.IsAdmin) return list;

            //获取权限数据
            var userRoles = _sysUserRoleService.GetAll();
            if (userRoles == null || !userRoles.Any()) return null;
            var roleIds = userRoles.Where(o => o.UserId == user.Id).Select(x => x.RoleId).Distinct().ToList();
            var permissionList = _sysPermissionServices.GetAll();
            if (permissionList == null || !permissionList.Any()) return null;

            var categoryIds = permissionList.Where(o => roleIds.Contains(o.RoleId)).Select(x => x.CategoryId).Distinct().ToList();
            if (!categoryIds.Any())
                return null;
            list = list.Where(o => categoryIds.Contains(o.Id)).ToList();
            return list;
        }

        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool authorize(ActionExecutingContext context)
        {
            var user = GetCurrentUser();
            if (user == null)
                return false;
            //如果是超级管理员
            //if (user.IsAdmin) return true;
            string action = context.ActionDescriptor.RouteValues["action"];
            string controller = context.ActionDescriptor.RouteValues["controller"];

            return authorize(action, controller);
        }

        /// <summary>
        /// 私有方法，判断权限
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        private bool authorize(string action, string controller)
        {
            var user = GetCurrentUser();
            if (user == null)
                return false;
            //如果是超级管理员
            if (user.IsAdmin) return true;
            var list = getMyCategories(user);
            if (list == null) return false;
            return list.Any(o => o.Controller != null && o.Action != null ||
            o.Controller.Equals(controller, StringComparison.InvariantCultureIgnoreCase)
            && o.Action.Equals(action, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="routeName"></param>
        /// <returns></returns>
        public bool authorize(string routeName)
        {
            var user = GetCurrentUser();
            if (user == null)
                return false;
            //如果是超级管理员
            if (user.IsAdmin) return true;
            var list = getMyCategories(user);
            if (list == null) return false;
            return list.Any(o => o.RouteName != null &&
            o.RouteName.Equals(routeName, StringComparison.InvariantCultureIgnoreCase));
        }



    }
}
