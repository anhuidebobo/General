using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using General.Mvc.Models;
using General.Entities;
using General.Services.Categorys;
using General.Core;

namespace General.Mvc.Controllers
{
    public class HomeController : Controller
    {
        //private ICategoryService _categoryService;
        //public HomeController(ICategoryService categoryService)
        //{
        //    _categoryService = categoryService;
        //}
        public IActionResult Index()
        {
            var categoryService = EngineContext.Current.Resolve<ICategoryService>();

            //var list = _categoryService.getAll();
            //return Content(list.ToString());
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
