using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using General.Core;
using General.Entities;

namespace General.Services
{
    public class CatergoyService : ICategoryService, IClone
    {
        //private GeneralDbContext _generalDbContext;  
        //public CatergoyService(GeneralDbContext generalDbContext)
        //{
        //    _generalDbContext = generalDbContext;    
        //} 
        private IRepository<Category> _repository;
        public CatergoyService(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public List<Category> getAll()
        {
            return _repository.Table.ToList();
        }
    }
}
