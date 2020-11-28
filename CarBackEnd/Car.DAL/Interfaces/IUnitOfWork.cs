using System;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Interfaces
{
    public interface IUnitOfWork<TEntity> : IDisposable where TEntity:class
    {
        DbContext db { get; }
    }
}
