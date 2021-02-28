using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly IUnitOfWork<LocationType> locationTypeUnitOfWork;

        public LocationTypeService(IUnitOfWork<LocationType> locationTypeUnitOfWork)
        {
            this.locationTypeUnitOfWork = locationTypeUnitOfWork;
        }

        public IEnumerable<LocationType> GetAllLocationTypes()
        {
            return locationTypeUnitOfWork.GetRepository().Query();
        }
    }
}
