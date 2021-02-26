using System;
using System.Threading.Tasks;
using Car.Data.Context;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Infrastructure
{
    public sealed class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext context;
        private readonly IRepository<TEntity> repository;

        public UnitOfWork(CarContext context)
        {
            this.context = context;
            repository = new Repository<TEntity>(context);
        }

        /// <summary>
        /// Gets DBContext, is used for disposing
        /// </summary>
        public DbContext db => context;

        /// <summary>
        /// Gets repository for TEntity
        /// </summary>
        /// <returns>instance of repository</returns>
        public IRepository<TEntity> GetRepository() => repository;

        /// <summary>
        /// Saves changes in DB
        /// </summary>
        public void SaveChanges() => context.SaveChanges();

        public Task SaveChangesAsync() => context.SaveChangesAsync();

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
