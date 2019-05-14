using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Admin.Controllers;
using General.Framework.Security.Admin;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    public class MainController : PublicAdminController
    {
        private IAdminAuthService _adminAuthService;
        public MainController(IAdminAuthService adminAuthService)
        {
            this._adminAuthService = adminAuthService;
        }
        public IActionResult Index()
        {
            //var user = WorkContext.CurrentUser;
            _adminAuthService.GetCurrentUser();

            return View();
        }
    }
}