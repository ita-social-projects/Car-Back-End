using System;
using Car.DAL.Context;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Infrastructure
{
    public sealed class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext context;

        public UnitOfWork(CarContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Gets DBContext, is used for disposing
        /// </summary>
        public DbContext db => context;

        /// <summary>
        /// Gets repository for TEntity
        /// </summary>
        /// <returns>instance of repository</returns>
        public IRepository<TEntity> GetRepository()
        {
            return new Repository<TEntity>(context);
        }

        /// <summary>
        /// Saves changes in DB
        /// </summary>
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                db.Dispose();
            }

            disposed = true;
        }
    }
}
