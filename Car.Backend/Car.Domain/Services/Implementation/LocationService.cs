using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.Location;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> locationRepository;
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocationService(IRepository<Location> locationRepository, IRepository<User> userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.locationRepository = locationRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<Location> GetLocationByIdAsync(int locationId) =>
            await locationRepository.Query().Include(locationAddress => locationAddress.Address).Include(locationType => locationType.Type).FirstOrDefaultAsync(i => i.Id == locationId);

        public async Task<IEnumerable<Location>> GetAllByUserIdAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            return await locationRepository
                .Query(locationAddress => locationAddress!.Address!, locationType => locationType!.Type!)
                .Where(location => location.UserId == userId)
                .ToListAsync();
        }

        public async Task<Location> AddLocationAsync(CreateLocationDto createLocationDto)
        {
            var location = mapper.Map<CreateLocationDto, Location>(createLocationDto);
            location.UserId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            var newLocation = await locationRepository.AddAsync(location);
            await locationRepository.SaveChangesAsync();

            return newLocation;
        }

        public async Task<(bool IsUpdated, Location? UpdatedLocation)> UpdateAsync(UpdateLocationDto location)
        {
            var updatedLocation = await locationRepository.Query().Include(locationAddress => locationAddress.Address).FirstOrDefaultAsync(i => i.Id == location.Id);

            if (updatedLocation is not null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != updatedLocation.UserId)
                {
                    return (false, null);
                }

                updatedLocation.Name = location.Name;
                updatedLocation!.Address!.Name = location!.Address!.Name;
                updatedLocation.Address.Longitude = location.Address.Longitude;
                updatedLocation.Address.Latitude = location.Address.Latitude;
                updatedLocation.TypeId = location.TypeId;

                await locationRepository.SaveChangesAsync();
            }

            return (true, updatedLocation);
        }

        public async Task<bool> DeleteAsync(int locationId)
        {
            var location = await locationRepository.GetByIdAsync(locationId);

            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            if (userId != location.UserId)
            {
                return false;
            }

            locationRepository.Delete(location);
            await locationRepository.SaveChangesAsync();

            return true;
        }
    }
}