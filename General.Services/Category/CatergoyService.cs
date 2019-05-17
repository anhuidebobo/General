using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Core;
using General.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace General.Services
{
    public class CatergoyService : ICategoryService, IClone
    {
        //private GeneralDbContext _generalDbContext;  
        //public CatergoyService(GeneralDbContext generalDbContext)
        //{
        //    _generalDbContext = generalDbContext;    
        //} 
        private const string MODEL_KEY = "General.services.category";

        private IMemoryCache _memoryCache;
        private IRepository<Category> _categoryRepository;
        private IRepository<SysPermission> _permissionRepository;
        public CatergoyService(IMemoryCache memoryCache, IRepository<Category> repository, IRepository<SysPermission> permissionRepository)
        {
            _memoryCache = memoryCache;
            _categoryRepository = repository;
            _permissionRepository = permissionRepository;
        }
        public List<Category> GetAll()
        {
            List<Category> list = null;
            _memoryCache.TryGetValue<List<Category>>(MODEL_KEY, out list);
            if (list != null)
                return list;
            list = _categoryRepository.Table.ToList();
            _memoryCache.Set(MODEL_KEY, list, DateTimeOffset.Now.AddDays(1));
            return list;
        }

        public void InitCategory(List<Category> list)
        {
            var oldList = _categoryRepository.Table.ToList();
            oldList.ForEach(del =>
            {
                var item = list.FirstOrDefault(o => o.SysResource == del.SysResource);
                if (item == null)
                {
                    var permissionList = del.SysPermissions.ToList();
                    permissionList.ForEach(delPrm =>
                    {
                        _permissionRepository.Entities.Remove(delPrm);
                    });
                    _categoryRepository.Entities.Remove(del);
                }
            });
            list.ForEach(entity =>
            {
                var item = oldList.FirstOrDefault(o => o.SysResource == entity.SysResource);
                if (item == null)
                {
                    _categoryRepository.Entities.Add(entity);
                }
                else
                {
                    item.Action = entity.Action;
                    item.Controller = entity.Controller;
                    item.CssClass = entity.CssClass;
                    item.FatherResource = entity.FatherResource;
                    item.IsMenu = entity.IsMenu;
                    item.Name = entity.Name;
                    item.RouteName = entity.RouteName;
                    item.SysResource = entity.SysResource;
                    item.Sort = entity.Sort;
                    item.FatherID = entity.FatherID;
                    item.ResourceID = entity.ResourceID;
                }
            });
            if (_categoryRepository.DbContext.ChangeTracker.HasChanges())
                _categoryRepository.DbContext.SaveChanges();
        }
    }
}
