using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Core.Librs;
using General.Entities;
using General.Framework.Admin.Controllers;
using General.Framework.Security.Admin;
using General.Services.SysUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("admin")]
    public class LoginController : AdminAreaController
    {
        private ISysUserService _sysUserService;
        private IAdminAuthService _authenticationService;
        public LoginController(ISysUserService sysUserService, IAdminAuthService authenticationService)
        {
            this._sysUserService = sysUserService;
            this._authenticationService = authenticationService;
        }
        //private const string R_KEY = "R_KEY";

        [Route("login", Name = "adminLogin")]
        public IActionResult Index()
        {
            //string r = EncryptorHelper.GetMD5(Guid.NewGuid().ToString());
            //HttpContext.Session.SetString(R_KEY, r);
            //LoginModel loginModel = new LoginModel() { R = r };
            //return View(loginModel);
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Index(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                AjaxData.Message = "请输入用户账号和密码！";
                return Json(AjaxData);
            }
            var result = this._sysUserService.ValidUser(model.Account, model.Password, "");
            AjaxData.Status = result.Item1;
            if (result.Item1)
            {
                _authenticationService.SingIn(result.Item3, result.Item4.Name);
            }
            return Json(AjaxData);
        }
    }
}