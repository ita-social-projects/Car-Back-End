using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Location;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByIdAsync(int locationId);

        Task<Location> AddLocationAsync(LocationDto locationDTO);

        Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId);

        Task<Location> UpdateAsync(UpdateLocationDto location);

        public Task DeleteAsync(int locationId);
    }
}
