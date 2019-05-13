using General.Core;
using General.Framework.Filters;
using General.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Admin.Controllers
{
    [AdminAuthFilter]
    public class PublicAdminController : AdminAreaController
    {
        private IWorkContext _workContext;
        public PublicAdminController()
        {
            this._workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }
    }
}
