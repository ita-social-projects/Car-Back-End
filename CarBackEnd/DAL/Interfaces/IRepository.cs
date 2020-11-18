using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IEntityBase
    {
    }
}
