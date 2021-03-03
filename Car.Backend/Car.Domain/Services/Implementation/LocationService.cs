using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepository;

        public LocationService(IRepository<Location> locationRepository) =>
            this.locationRepository = locationRepository;

        public async Task<Location> GetLocationByIdAsync(int locationId) =>
            await locationRepository.Query().FirstOrDefaultAsync(i => i.Id == locationId);

        public async Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId) =>
            locationRepository
                .Query(locationAddress => locationAddress.Address, locationType => locationType.Type)
                .Where(location => location.UserId == userId);

        public async Task<Location> AddLocationAsync(Location location)
        {
            var addedLocation = locationRepository.AddAsync(location);
            locationRepository.SaveChangesAsync();

            return await addedLocation;
        }

        public async Task<Location> UpdateLocationAsync(Location location)
        {
            var updatedLocation = locationRepository.UpdateAsync(location);
            locationRepository.SaveChangesAsync();

            return await updatedLocation;
        }
    }
}