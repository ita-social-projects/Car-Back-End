using System;
using System.Threading.Tasks;
using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.Data.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable
        where TEntity : class, IEntity
    {
        DbContext db { get; }

        IRepository<TEntity> GetRepository();

        void SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
