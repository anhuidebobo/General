﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Framework.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace General.Mvc.Areas.Admin.Controllers
{
    public class MainController : PublicAdminController
    {
        public IActionResult Index()
        {
            var user = WorkContext.CurrentUser;

            return View();
        }
    }
}