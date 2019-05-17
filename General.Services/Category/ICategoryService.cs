﻿using General.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        void InitCategory(List<Category> list);
    }

    public interface IClone { }
}
