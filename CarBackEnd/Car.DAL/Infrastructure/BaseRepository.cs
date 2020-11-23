using Car.DAL.Context;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Car.DAL.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        internal readonly CarContext context;
        internal DbSet<TEntity> entities;

        public BaseRepository(CarContext _context)
        {
            context = _context;
            entities = context.Set<TEntity>();
        }

        public TEntity Add(TEntity entity)
        {
            return entities.Add(entity) as TEntity;
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            entities.AddRange(entity);
        }

        public bool Delete(TEntity entityToDelete)
        {
            return entities.Remove(entityToDelete) != null;
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            entities.RemoveRange(entity);
        }

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

        public TEntity GetById(params object[] keys)
        {
            return entities.Find(keys);
        }

        public TEntity Update(TEntity entityToUpdate)
        {
            return entities.Update(entityToUpdate) as TEntity;
        }
    }
}
