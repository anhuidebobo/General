using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace General.Core
{
    public interface IRepository<TEntity> where TEntity : class
    {
        DbContext DbContext { get; }
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> Table { get; }

        TEntity GetEntityById(object id);

        void Insert(TEntity entity, bool isSave = true);

        void Update(TEntity entity, bool isSave = true);

        void Delete(TEntity entity, bool isSave = true);
    }
}
