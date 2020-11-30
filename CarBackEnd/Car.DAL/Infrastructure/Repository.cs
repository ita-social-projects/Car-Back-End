﻿using Car.DAL.Context;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Car.DAL.Infrastructure
{
    public sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext context;
        private DbSet<TEntity> dbEntities;

        public Repository(CarContext _context)
        {
            context = _context;
            dbEntities = context.Set<TEntity>();
        }

        /// <summary>
        /// Adds entity into DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>added entity</returns>
        public TEntity Add(TEntity entity)
        {
            return dbEntities.Add(entity) as TEntity;
        }

        /// <summary>
        /// Adds range of entities into DBContext
        /// </summary>
        /// <param name="entities">IEnumarable of entities to add</param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            dbEntities.AddRange(entities);
        }

        /// <summary>
        /// Removes entity from DBContext
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>true if entity was successfully deleted, and false in other way</returns>
        public bool Delete(TEntity entity)
        {
            return dbEntities.Remove(entity) != null;
        }

        /// <summary>
        /// Removes range of entities from DBContext
        /// </summary>
        /// <param name="entities">IEnumarable of entities</param>
        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            dbEntities.RemoveRange(entities);
        }

        /// <summary>
        /// Joins entity with entities in parameters
        /// </summary>
        /// <param name="includes">entity to join</param>
        /// <returns>IQueryable of this entity or joined with includes</returns>
        public IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] includes)
        {
            DbSet<TEntity> dbSet = context.Set<TEntity>();
            IQueryable<TEntity> query = null;
            foreach (var include in includes)
            {
                query = dbSet.Include(include);
            }

            return query ?? dbSet;
        }

        /// <summary>
        /// Finds and returns TEntity based on Primary Key
        /// </summary>
        /// <param name="keys">Primary Key</param>
        /// <returns>entity with id = keys</returns>
        public TEntity GetById(params object[] keys)
        {
            return dbEntities.Find(keys);
        }

        /// <summary>
        /// Updates entity
        /// </summary>
        /// <param name="entity">entity</param>
        /// <returns>updated entity</returns>
        public TEntity Update(TEntity entity)
        {
            return dbEntities.Update(entity) as TEntity;
        }
    }
}
