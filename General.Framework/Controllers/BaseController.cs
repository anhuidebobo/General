using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Controllers
{
    public abstract class BaseController : Controller
    {
        private AjaxResult _ajaxResult = new AjaxResult();

        public AjaxResult AjaxData
        {
            get
            {
                return _ajaxResult;
            }
        }
    }
}
