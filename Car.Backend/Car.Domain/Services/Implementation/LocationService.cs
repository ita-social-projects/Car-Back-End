using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Models.Location;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepository;
        private readonly IMapper mapper;

        public LocationService(IRepository<Location> locationRepository, IMapper mapper)
        {
            this.locationRepository = locationRepository;
            this.mapper = mapper;
        }

        public async Task<Location> GetLocationByIdAsync(int locationId) =>
            await locationRepository.Query().Include(locationAddress => locationAddress.Address).Include(locationType => locationType.Type).FirstOrDefaultAsync(i => i.Id == locationId);

        public async Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId)
        {
            return await locationRepository
                .Query(locationAddress => locationAddress.Address, locationType => locationType.Type)
                .Where(location => location.UserId == userId)
                .ToListAsync();
        }

        public async Task<Location> AddLocationAsync(CreateLocationModel createLocationModel)
        {
            var location = mapper.Map<CreateLocationModel, Location>(createLocationModel);

            var newLocation = await locationRepository.AddAsync(location);
            await locationRepository.SaveChangesAsync();

            return newLocation;
        }

        public async Task<Location> UpdateLocationAsync(Location location)
        {
            var updatedLocation = await locationRepository.UpdateAsync(location);
            await locationRepository.SaveChangesAsync();

            return updatedLocation;
        }
    }
}