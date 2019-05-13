using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Mvc.Models;
using General.Entities;
using General.Core;
using General.Services;
using General.Framework.Controllers;

namespace General.Mvc.Controllers
{
    public class HomeController : BaseController
    {
        //private IRepository<Category> _categoryRepository;
        //private IRepository<SysUser> _sysUserRepository;
        //public HomeController(IRepository<Category> categoryRepository,
        // IRepository<SysUser> sysUserRepository)
        //{
        //    _categoryRepository = categoryRepository;
        //    _sysUserRepository = sysUserRepository;
        //}
        public IActionResult Index()
        {
            //bool result = Object.ReferenceEquals(_categoryRepository.DbContext, _sysUserRepository.DbContext);

            var categoryService = EngineContext.Current.Resolve<ICategoryService>();

            var list = categoryService.getAll();
            return Content(categoryService.ToString());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
