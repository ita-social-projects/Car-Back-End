using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Location;
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

        public async Task<Location> AddLocationAsync(LocationDto locationDTO)
        {
            var location = mapper.Map<LocationDto, Location>(locationDTO);

            var newLocation = await locationRepository.AddAsync(location);
            await locationRepository.SaveChangesAsync();

            return newLocation;
        }

        public async Task<Location> UpdateAsync(UpdateLocationDto location)
        {
            var updatedLocation = await locationRepository.Query().Include(locationAddress => locationAddress.Address).FirstOrDefaultAsync(i => i.Id == location.Id);

            if (updatedLocation is not null)
            {
                updatedLocation.Name = location.Name;
                updatedLocation.Address.Name = location.Address.Name;
                updatedLocation.Address.Longitude = location.Address.Longitude;
                updatedLocation.Address.Latitude = location.Address.Latitude;
                updatedLocation.TypeId = location.TypeId;
            }

            await locationRepository.SaveChangesAsync();

            return updatedLocation;
        }

        public async Task DeleteAsync(int locationId)
        {
            locationRepository.Delete(new Location() { Id = locationId });
            await locationRepository.SaveChangesAsync();
        }
    }
}