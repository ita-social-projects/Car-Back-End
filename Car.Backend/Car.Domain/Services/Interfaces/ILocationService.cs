using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByIdAsync(int locationId);

        Task<Location> AddLocationAsync(Location location);

        Task<IEnumerable<Location>> GetAllByUserIdAsync(int locationId);

        Task<Location> UpdateLocationAsync(Location location);
    }
}
