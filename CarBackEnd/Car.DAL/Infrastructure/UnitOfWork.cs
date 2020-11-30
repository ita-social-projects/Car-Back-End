using Car.DAL.Context;
using Car.DAL.Interfaces;
using Car.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Car.DAL.Infrastructure
{
    public sealed class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : class, IEntity
    {
        private CarContext context;

        public UnitOfWork(CarContext _context)
        {
            context = _context;
        }

        /// <summary>
        /// Gets DBcontext, is used for disposing
        /// </summary>
        public DbContext db { get => context; }

        /// <summary>
        /// Gets repository for TEntity
        /// </summary>
        /// <returns>instance of repository</returns>
        public Repository<TEntity> GetRepository()
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
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            disposed = true;
        }
    }
}
