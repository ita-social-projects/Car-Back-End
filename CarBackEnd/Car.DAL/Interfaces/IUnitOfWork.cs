using System;
using Car.DAL.Entities;
using Car.DAL.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        DbContext db { get; }

        IRepository<TEntity> GetRepository();

        Repository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;

        void SaveChanges();
    }
}
