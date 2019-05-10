using General.Entities.Categorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services.Categorys
{
    public interface ICategoryService
    {
        List<Category> getAll();
    }
}
