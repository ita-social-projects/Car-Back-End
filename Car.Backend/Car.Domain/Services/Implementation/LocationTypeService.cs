using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class LocationTypeService : ILocationTypeService
    {
        private readonly IRepository<LocationType> locationTypeRepository;

        public LocationTypeService(IRepository<LocationType> locationTypeRepository)
        {
            this.locationTypeRepository = locationTypeRepository;
        }

        public async Task<IEnumerable<LocationType>> GetAllLocationTypesAsync()
        {
            return locationTypeRepository.Query();
        }
    }
}
