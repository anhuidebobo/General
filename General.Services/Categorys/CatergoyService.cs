using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Entities;
using General.Entities.Categorys;

namespace General.Services.Categorys
{
    public class CatergoyService : ICategoryService
    {
        private GeneralDbContext _generalDbContext;  
        public CatergoyService(GeneralDbContext generalDbContext)
        {
            _generalDbContext = generalDbContext;    
        } 
        public List<Category> getAll()
        {
            return _generalDbContext.Categories.ToList();
        }
    }
}
