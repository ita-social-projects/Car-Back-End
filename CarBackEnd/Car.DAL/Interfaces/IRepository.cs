using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Car.DAL.Entities;

namespace Car.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Filters data, if filter != null, in other way gets all TEntity
        /// </summary>
        /// <param name="filter">Filters used in Where</param>
        /// <returns>IEnumarable of entity framework's entities</returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null);

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
        void Add(TEntity entity);

        /// <summary>
        /// Adds range of entities into DBContext
        /// </summary>
        /// <param name="entity">IEnumarable of entity framework's entity to add</param>
        void AddRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Removes entity from DBContext
        /// </summary>
        /// <param name="entityToDelete">entity framework's entity to delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Removes range of entities from DBContext
        /// </summary>
        /// <param name="entity">IEnumarable of entity framework's entity to delete</param>
        void DeleteRange(IEnumerable<TEntity> entity);

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entityToUpdate">entity framework's entity to update</param>
        void Update(TEntity entityToUpdate);
    }
}
