using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Car.Data.Infrastructure
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task DeleteRangeAsync(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> SaveChangesAsync();

        ValueTask<TEntity> GetByIdAsync(params object[] keys);

        void Delete(TEntity entity);
    }
}
