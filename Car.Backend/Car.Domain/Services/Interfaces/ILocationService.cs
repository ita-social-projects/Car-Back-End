using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Models.Location;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Task<Location> GetLocationByIdAsync(int locationId);

        Task<Location> AddLocationAsync(CreateLocationModel createLocationModel);

        Task<IEnumerable<Location>> GetAllByUserIdAsync(int userId);

        Task<Location> UpdateLocationAsync(Location location);
    }
}
