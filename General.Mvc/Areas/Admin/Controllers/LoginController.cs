using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    [Route("admin/login")]
    public class LoginController : AdminAreaController
    {
        [Route("", Name = "adminLogin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}