using System;
using Car.Data.Context;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Infrastructure
{
    public sealed class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext _context;

        public UnitOfWork(CarContext context) => this._context = context;

        /// <summary>
        /// Gets DBContext, is used for disposing
        /// </summary>
        public DbContext db => _context;

        /// <summary>
        /// Gets repository for TEntity
        /// </summary>
        /// <returns>instance of repository</returns>
        public IRepository<TEntity> GetRepository() => new Repository<TEntity>(_context);

        /// <summary>
        /// Saves changes in DB
        /// </summary>
        public void SaveChanges() => _context.SaveChanges();

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
