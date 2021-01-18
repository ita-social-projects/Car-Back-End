﻿using Microsoft.EntityFrameworkCore;
using System;
using Car.Data.Context;
using Car.Data.Entities;
using Car.Data.Interfaces;

namespace Car.Data.Infrastructure
{
    public sealed class UnitOfWork<TEntity> : IUnitOfWork<TEntity>
        where TEntity : class, IEntity
    {
        private readonly CarContext context;

        public UnitOfWork(CarContext _context) => context = _context;

        /// <summary>
        /// Gets DBcontext, is used for disposing
        /// </summary>
        public DbContext db => context;

        /// <summary>
        /// Gets repository for TEntity
        /// </summary>
        /// <returns>instance of repository</returns>
        public IRepository<TEntity> GetRepository() => new Repository<TEntity>(context);

        /// <summary>
        /// Saves changes in DB
        /// </summary>
        public void SaveChanges() => context.SaveChanges();

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