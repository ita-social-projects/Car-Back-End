using System;
using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork<Location> locationUnitOfWork;

        public LocationService(IUnitOfWork<Location> locationUnitOfWork)
        {
            this.locationUnitOfWork = locationUnitOfWork;
        }

        public Location GetLocationById(int locationId)
        {
            return locationUnitOfWork.GetRepository().Query().FirstOrDefault(i => i.Id == locationId);
        }

        public IEnumerable<Location> GetAllByUserId(int userId)
        {
            return locationUnitOfWork.GetRepository()
                .Query(locationAddress => locationAddress.Address, locationType => locationType.Type)
                .Where(location => location.UserId == userId);
        }

        public Location AddLocation(Location location)
        {
            var addedLocation = locationUnitOfWork.GetRepository().Add(location);
            locationUnitOfWork.SaveChanges();

            return addedLocation;
        }

        public Location UpdateLocation(Location location)
        {
            var updatedLocation = locationUnitOfWork.GetRepository().Update(location);
            locationUnitOfWork.SaveChanges();

            return updatedLocation;
        }
    }
}