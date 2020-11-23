using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Car.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Joins entity with entities in parameters
        /// </summary>
        /// <param name="includes">entity to join</param>
        /// <returns>IQueryable of this entity or joined with includes</returns>
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Finds and returns TEntity based on Primary Key
        /// </summary>
        /// <param name="keys">Primary Key</param>
        /// <returns>entity framework's entity</returns>
        TEntity GetById(params object[] keys);

        /// <summary>
        /// Adds entity into DBContext
        /// </summary>
        /// <param name="entity">entity framework's entity to add</param>
        /// <returns>added entity framework's entity</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Adds range of entities into DBContext
        /// </summary>
        /// <param name="entity">IEnumarable of entity framework's entity to add</param>
        void AddRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Removes entity from DBContext
        /// </summary>
        /// <param name="entityToDelete">entity framework's entity to delete</param>
        /// <returns>true if entity was successfully deleted, and false in other way</returns>
        bool Delete(TEntity entityToDelete);

        /// <summary>
        /// Removes range of entities from DBContext
        /// </summary>
        /// <param name="entity">IEnumarable of entity framework's entity to delete</param>
        void DeleteRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityToUpdate">entity framework's entity to update</param>
        /// <returns>updated entity framework's entity</returns>
        TEntity Update(TEntity entityToUpdate);
    }
}
