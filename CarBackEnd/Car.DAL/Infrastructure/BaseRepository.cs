using Car.DAL.Context;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Car.DAL.Infrastructure
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityBase
    {
        internal DbSet<TEntity> entities;
        private readonly UnitOfWork unitOfWork;

        public BaseRepository(UnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
            entities = _unitOfWork.db.Set<TEntity>();
        }
        public void Add(TEntity entity)
        {
            entities.Add(entity);
            unitOfWork.Save();
        }

        public void AddRange(IEnumerable<TEntity> entity)
        {
            entities.AddRange(entity);
            unitOfWork.Save();
        }

        public void Delete(TEntity entityToDelete)
        {
            entities.Remove(entityToDelete);
            unitOfWork.Save();
        }

        public void DeleteRange(IEnumerable<TEntity> entity)
        {
            entities.RemoveRange(entity);
            unitOfWork.Save();
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null)
        {
            IQueryable<TEntity> query = entities;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return query.ToList();
        }

        public TEntity GetById(params object[] keys)
        {
            return entities.Find(keys);
        }

        public void Update(TEntity entityToUpdate)
        {
            entities.Update(entityToUpdate);
            unitOfWork.Save();
        }
    }
}
