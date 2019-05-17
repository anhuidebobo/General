using General.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace General.Framework.Menu.Register
{
    public class RegisterApplicationService : IRegisterApplicationService
    {
        private ICategoryService _categoryService;

        public RegisterApplicationService(ICategoryService categoryService)
        {
            this._categoryService = categoryService;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void InitRegister()
        {
            List<Entities.Category> list = new List<Entities.Category>();
            FunctionManager.getFunctionLists().ForEach(item =>
            {
                list.Add(new Entities.Category()
                {
                    Id = Guid.NewGuid(),
                    Action = item.Action,
                    Controller = item.Controller,
                    CssClass = item.CssClass,
                    FatherResource = item.FatherResource,
                    IsMenu = item.IsMenu,
                    Name = item.Name,
                    RouteName = item.RouteName,
                    SysResource = item.SysResource,
                    Sort = item.Sort,
                    FatherID = item.FatherID,
                    IsDisabled = false,
                    ResourceID = item.ResourceID
                });
            });
            _categoryService.InitCategory(list);
        }
    }
}
