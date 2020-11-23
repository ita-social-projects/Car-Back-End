using System;
using Microsoft.EntityFrameworkCore;

namespace Car.DAL.Interfaces
{
    public class IUnitOfWork : IDisposable
    {
        DbContext db { get; }
        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
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
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
