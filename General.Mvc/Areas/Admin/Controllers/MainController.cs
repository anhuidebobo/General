using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Admin.Controllers;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("admin/main")]
    public class MainController : PublicAdminController
    {
        private IAdminAuthService _adminAuthService;
        public MainController(IAdminAuthService adminAuthService)
        {
            this._adminAuthService = adminAuthService;
        }
        [Route("", Name = "mainIndex")]
        public IActionResult Index()
        {
            //var user = WorkContext.CurrentUser;
            _adminAuthService.GetCurrentUser();

            return View();
        }
        [Route("out", Name = "signOut")]
        public IActionResult SignOut()
        {
            _adminAuthService.SignOut();
            return RedirectToRoute("adminLogin");
        }
    }
}