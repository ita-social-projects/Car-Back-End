using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Data.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        TEntity GetById(params object[] keys);

        TEntity Add(TEntity entity);

        Task<TEntity> AsyncAdd(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities);

        bool Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        Task<Int32> AsyncUpdate(TEntity entity);
    }
}
