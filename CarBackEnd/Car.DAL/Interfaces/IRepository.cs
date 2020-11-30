using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Car.DAL.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        TEntity GetById(params object[] keys);

        TEntity Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entity);

        bool Delete(TEntity entityToDelete);

        void DeleteRange(IEnumerable<TEntity> entity);

        TEntity Update(TEntity entityToUpdate);
    }
}
