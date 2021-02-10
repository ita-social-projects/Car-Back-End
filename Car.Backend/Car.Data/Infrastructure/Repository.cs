using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Car.Data.Context;
using Car.Data.Entities;
using Car.Data.Interfaces;
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
        /// Adds entity into DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>added entity</returns>
        public TEntity Add(TEntity entity) => dbEntities.Add(entity).Entity;

        /// <summary>
        /// Async add entity into DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>added entity</returns>
        public async Task<TEntity> AsyncAdd(TEntity entity) => (await dbEntities.AddAsync(entity)).Entity;

        /// <summary>
        /// Adds range of entities into DBContext
        /// </summary>
        /// <param name="entities">IEnumerable of entities to add</param>
        public void AddRange(IEnumerable<TEntity> entities) => dbEntities.AddRange(entities);

        /// <summary>
        /// Removes entity from DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>true if entity was successfully deleted, and false in other way</returns>
        public bool Delete(TEntity entity) => dbEntities.Remove(entity).Entity != null;

        /// <summary>
        /// Removes range of entities from DBContext
        /// </summary>
        /// <param name="entities">IEnumerable of entities</param>
        public void DeleteRange(IEnumerable<TEntity> entities) => dbEntities.RemoveRange(entities);

        /// <summary>
        /// Gets all entity records with included entities
        /// </summary>
        /// <param name="includes">included entities</param>
        /// <returns>IQueryable of all entity records with included entities, if includes is null this function is equal GetAll</returns>
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(dbSet, (current, include) => current.Include(include));

            return query ?? dbSet;
        }

        /// <summary>
        /// Finds and returns TEntity based on Primary Key
        /// </summary>
        /// <param name="keys">Primary Key</param>
        /// <returns>entity with id = keys</returns>
        public TEntity GetById(params object[] keys) => dbEntities.Find(keys);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>updated entity</returns>
        public TEntity Update(TEntity entity) => dbEntities.Update(entity).Entity;

        public async Task<Int32> AsyncUpdate(TEntity entity) => await Task.Run(async () =>
        {
            dbEntities.Update(entity);
            return await context.SaveChangesAsync();
        });
    }
}
