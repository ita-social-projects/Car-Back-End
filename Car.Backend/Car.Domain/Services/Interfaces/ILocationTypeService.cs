using System.Collections.Generic;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationTypeService
    {
        public IEnumerable<LocationType> GetAllLocationTypes();
    }
}
