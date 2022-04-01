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

        Task<(bool IsAdded, Location? AddedLocation)> AddLocationAsync(LocationDto locationDTO);

        Task<IEnumerable<Location>> GetAllByUserIdAsync();

        Task<(bool IsUpdated, Location? UpdatedLocation)> UpdateAsync(UpdateLocationDto location);

        public Task<bool> DeleteAsync(int locationId);
    }
}
