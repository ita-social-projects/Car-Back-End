using System.Collections.Generic;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        Location GetLocationById(int locationId);

        Location AddLocation(Location location);

        IEnumerable<Location> GetAllByUserId(int locationId);

        Location UpdateLocation(Location location);
    }
}
