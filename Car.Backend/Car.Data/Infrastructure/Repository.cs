using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Car.Data.Context;
using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Infrastructure
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext context;
        private readonly DbSet<TEntity> dbEntities;

        public Repository(CarContext context)
        {
            this.context = context;
            dbEntities = this.context.Set<TEntity>();
        }

        /// <summary>
        /// Gets all entity records with included entities
        /// </summary>
        /// <param name="includes">included entities</param>
        /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll</returns>
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = context.Set<TEntity>();
            var query = includes
                .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(dbSet, (current, include) => current.Include(include));

            return query ?? dbSet;
        }

        /// <summary>
        /// Gets entity by the keys.
        /// </summary>
        /// <param name="keys">Keys for the search.</param>
        /// <returns>Entity with such keys.</returns>
        public ValueTask<TEntity> GetByIdAsync(params object[] keys) => dbEntities.FindAsync(keys);

        /// <summary>
        /// Async add entity into DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <exception cref="ArgumentNullException">The entity to add cannot be <see langword="null"/>.</exception>
        /// <returns>added entity</returns>
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            CheckEntityForNull(entity);
            return (await dbEntities.AddAsync(entity)).Entity;
        }

        /// <summary>
        /// Adds a range of entities.
        /// </summary>
        /// <param name="entities">Entities to add.</param>
        /// <returns>Task.</returns>
        public Task AddRangeAsync(IEnumerable<TEntity> entities) => dbEntities.AddRangeAsync(entities);

        /// <summary>
        /// Updates entity asynchronously
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>awaitable task with updated entity</returns>
        public async Task<TEntity> UpdateAsync(TEntity entity) =>
            await Task.Run(() => dbEntities.Update(entity).Entity);

        /// <summary>
        /// Removes entity from DBContext Asynchronously
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>true if entity was successfully deleted, and false in other way</returns>
        public async Task<bool> DeleteAsync(TEntity entity) =>
            (await Task.Run(() => dbEntities.Remove(entity))).Entity != null;

        /// <summary>
        /// Deletes range.
        /// </summary>
        /// <param name="entities">Entities to delete.</param>
        /// <returns>Task.</returns>
        public Task DeleteRangeAsync(IEnumerable<TEntity> entities) =>
            Task.Run(() => dbEntities.RemoveRange());

        /// <summary>
        /// Saves changes in the database asynchronously.
        /// </summary>
        /// <returns>Task</returns>
        public Task<int> SaveChangesAsync() => context.SaveChangesAsync();

        public void AttachToDelete(TEntity entity) => context.Entry(entity).State = EntityState.Deleted;

        private static void CheckEntityForNull(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");
            }
        }
    }
}
