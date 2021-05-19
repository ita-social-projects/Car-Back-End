using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByIdAsync(int locationId);

        Task<Location> AddLocationAsync(LocationDTO locationDTO);

        Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId);

        Task<Location> UpdateLocationAsync(Location location);
    }
}
