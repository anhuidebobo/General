using General.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Entities
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private GeneralDbContext _generalDbContext;
        public EFRepository(GeneralDbContext generalDbContext)
        {
            _generalDbContext = generalDbContext;
        }

        public DbContext DbContext
        {
            get
            {
                return _generalDbContext;
            }
        }

        public DbSet<TEntity> Entities
        {
            get
            {
                return _generalDbContext.Set<TEntity>();
            }
        }

        public IQueryable<TEntity> Table
        {
            get
            {
                return _generalDbContext.Set<TEntity>();
            }
        }

        public void Delete(TEntity entity, bool isSave = true)
        {
            Entities.Remove(entity);
            if (isSave)
            {
                _generalDbContext.SaveChanges();
            }
        }

        public TEntity GetEntityById(object id)
        {
            return _generalDbContext.Set<TEntity>().Find(id);
        }

        public void Insert(TEntity entity, bool isSave = true)
        {
            Entities.Add(entity);
            if (isSave)
            {
                _generalDbContext.SaveChanges();
            }
        }

        public void Update(TEntity entity, bool isSave = true)
        {
            Entities.Update(entity);
            if (isSave)
            {
                _generalDbContext.SaveChanges();
            }
        }
    }
}
