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
            await locationRepository.Query().Include(locationAddress => locationAddress.Address).Include(locationType => locationType.Type).FirstOrDefaultAsync(i => i.Id == locationId);

        public async Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId)
        {
            return await locationRepository
                .Query(locationAddress => locationAddress.Address, locationType => locationType.Type)
                .Where(location => location.UserId == userId)
                .ToListAsync();
        }

        public async Task<Location> AddLocationAsync(Location location)
        {
            var addedLocation = await locationRepository.AddAsync(location);
            await locationRepository.SaveChangesAsync();

            return addedLocation;
        }

        public async Task<Location> UpdateLocationAsync(Location location)
        {
            var updatedLocation = await locationRepository.UpdateAsync(location);
            await locationRepository.SaveChangesAsync();

            return updatedLocation;
        }
    }
}