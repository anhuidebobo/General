using General.Entities.Categorys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace General.Entities
{
    public class GeneralDbContext : DbContext
    {
        public GeneralDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
    }
}
