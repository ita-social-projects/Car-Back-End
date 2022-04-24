using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Constants;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Domain.Dto.Address;
using Car.Domain.Dto.Stop;
using Car.Domain.Filters;
using Car.Domain.Models.Common;
using Car.Domain.Services.Interfaces;
using Geolocation;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Extensions
{
    public static class JourneyServiceExtension
    {
        public static IQueryable<Journey> FilterByUser(this IQueryable<Journey> journeys, int userId) =>
            journeys.Where(journey => journey.Participants.Any(user => user.Id == userId) || journey.OrganizerId == userId);

        public static IQueryable<Journey> IncludeStopsWithAddresses(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Stops.OrderBy(stop => stop.Index))
                .ThenInclude(stop => stop.Address);

        public static IQueryable<Journey> IncludeStopsWithUserStops(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Stops.OrderBy(stop => stop.Index))
                .ThenInclude(stop => stop.UserStops);

        public static IQueryable<Journey> IncludeStopsWithAddressesAndUserStops(this IQueryable<Journey> journeys) =>
            journeys.IncludeStopsWithAddresses().IncludeStopsWithUserStops();

        public static IQueryable<Journey> IncludeJourneyPoints(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.JourneyPoints.OrderBy(point => point.Index));

        public static IQueryable<Journey> IncludeJourneyInvitations(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Invitations);

        public static IQueryable<Journey> IncludeCar(this IQueryable<Journey> journeys) =>
    journeys.Include(journey => journey.Car);

        public static IQueryable<Journey> IncludeAllParticipants(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Organizer)
                .Include(journey => journey.Participants);

        public static IQueryable<Journey> IncludeJourneyInfo(this IQueryable<Journey> journeys, int userId) =>
            journeys.IncludeCar().IncludeAllParticipants().IncludeStopsWithAddressesAndUserStops().FilterByUser(userId);

        public static IQueryable<Journey> IncludeNotifications(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Notifications);

        public static IQueryable<Journey> IncludeSchedule(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Schedule);

        public static IQueryable<Journey> IncludeJourneyUsers(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.JourneyUsers);

        public static IQueryable<Journey> FilterPast(this IQueryable<Journey> journeys)
        {
            DateTime now = DateTime.UtcNow;

            return journeys.Where(journey =>
                journey.DepartureTime.AddHours(journey.Duration.Hours).AddMinutes(journey.Duration.Minutes) < now);
        }

        public static IQueryable<Journey> FilterUpcoming(this IQueryable<Journey> journeys)
        {
            DateTime now = DateTime.UtcNow;

            return journeys.Where(journey =>
                journey.DepartureTime > now);
        }

        public static IQueryable<Journey> FilterCanceled(this IQueryable<Journey> journeys)
        {
            return journeys.Where(journey => journey.IsCancelled);
        }

        public static IQueryable<Journey> FilterEditable(this IQueryable<Journey> journeys)
        {
            DateTime now = DateTime.UtcNow;

            return journeys.FilterUncancelledJourneys().IncludeSchedule().Where(journey =>
                journey.DepartureTime > now || journey.Schedule != null);
        }

        public static async Task<IQueryable<Journey>> UseSavedAdresses(this IQueryable<Journey> journeys, ILocationService locationService)
        {
            var savedLocations = await locationService.GetAllByUserIdAsync();

            journeys.ToList()
                .ForEach(journey => journey.Stops.Select(stop => stop.Address).ToList()
                    .ForEach(address => savedLocations
                        .Where(location => address is not null
                            && location.Address is not null
                            && address.Name == location.Address.Name
                            && address.Latitude == location.Address.Latitude
                            && address.Longitude == location.Address.Longitude).ToList()
                                .ForEach(location => address!.Name = location.Name)));

            return journeys;
        }

        public static IQueryable<IEnumerable<StopDto>> SelectStartAndFinishStops(this IQueryable<Journey> journeys)
        {
            var result = journeys.Select(journey => journey.Stops.Select(stop => new StopDto
            {
                Id = stop.Id,
                Address = new AddressDto
                {
                    Id = stop.Address!.Id,
                    Name = stop.Address.Name,
                    Longitude = stop.Address.Longitude,
                    Latitude = stop.Address.Latitude,
                },
            }));

            return result;
        }

        public static IQueryable<Journey> FilterUncancelledJourneys(this IQueryable<Journey> journeys) =>
            journeys.Where(journey => !journey.IsCancelled);

        public static IQueryable<Journey> FilterUnscheduledJourneys(this IQueryable<Journey> journeys) =>
            journeys.Where(journey => journey.Schedule == null);

        public static IQueryable<Journey> FilterScheduledJourneys(this IQueryable<Journey> journeys) =>
            journeys.Where(journey => journey.Schedule != null);

        public static IEnumerable<Request> FilterUnsuitableRequests(
            this IQueryable<Request> requests,
            Journey journey,
            Func<Request, JourneyFilter> requestToJourneyFilter) =>
            requests
                .Where(request => journey.DepartureTime > DateTime.UtcNow
                                  && journey.DepartureTime <=
                                  request.DepartureTime.AddHours(Constants.JourneySearchTimeScopeHours)
                                  && journey.DepartureTime >=
                                  request.DepartureTime.AddHours(-Constants.JourneySearchTimeScopeHours))
                .Where(request => (journey.IsFree && request.Fee == FeeType.Free)
                                  || (!journey.IsFree && request.Fee == FeeType.Paid)
                                  || (request.Fee == FeeType.All))
                .AsEnumerable()
                .Where(request => IsSuitablePoints(journey, requestToJourneyFilter(request)))
                .Where(request => IsSuitableSeatsCount(journey, requestToJourneyFilter(request)));

        public static IEnumerable<Journey> FilterUnsuitableJourneys(this IQueryable<Journey> journeys, JourneyFilter filter) =>
            journeys
                .Where(journey => journey.DepartureTime > DateTime.UtcNow
                                  && journey.DepartureTime <=
                                  filter.DepartureTime.AddHours(Constants.JourneySearchTimeScopeHours)
                                  && journey.DepartureTime >=
                                  filter.DepartureTime.AddHours(-Constants.JourneySearchTimeScopeHours))
                .Where(journey => (journey.IsFree && filter.Fee == FeeType.Free)
                                  || (!journey.IsFree && filter.Fee == FeeType.Paid)
                                  || (filter.Fee == FeeType.All))
                .AsEnumerable()
                .Where(journey => IsSuitablePoints(journey, filter))
                .Where(journey => IsSuitableSeatsCount(journey, filter));

        public static IQueryable<Journey> FilterUnmarked(this IQueryable<Journey> journeys)
        {
            return journeys.Where(journey => !journey.IsMarkedAsFinished);
        }

        public static double CalculateDistance(this JourneyPoint point, double latitude, double longitude) =>
            GeoCalculator.GetDistance(
                point.Latitude,
                point.Longitude,
                latitude,
                longitude,
                distanceUnit: DistanceUnit.Kilometers);

        public static double CalculateDistance(this Address address, double latitude, double longitude) =>
            GeoCalculator.GetDistance(
                address.Latitude,
                address.Longitude,
                latitude,
                longitude,
                distanceUnit: DistanceUnit.Kilometers);

        public static double CalculateDistance(double slatitude, double slongitude, double dlatitude, double dlongitude, int numberOfDecimalPalces = 1) =>
            GeoCalculator.GetDistance(
                slatitude,
                slongitude,
                dlatitude,
                dlongitude,
                distanceUnit: DistanceUnit.Kilometers,
                decimalPlaces: numberOfDecimalPalces);

        public static bool TryToGetSuitableDistance(this ICollection<JourneyPoint> journeyPoints, double latitude, double longitide, out SuitablePoint? suitablePoint)
        {
            var mostSuitablePoint = journeyPoints.Select(x => new { JourneyPoint = x, Distance = x.CalculateDistance(latitude, longitide) })
                .OrderBy(x => x.Distance)
                .First();

            if (mostSuitablePoint.Distance > Constants.JourneySearchRadiusKm)
            {
                suitablePoint = null;
                return false;
            }

            suitablePoint = new SuitablePoint(mostSuitablePoint.JourneyPoint, mostSuitablePoint.Distance);
            return true;
        }

        public static MergeStop? GetStopWithSuitableMergeAddress(this ICollection<Stop> stops, double latitude, double longitide) =>
            stops.Select(stop => new MergeStop(stop, stop.Address!.CalculateDistance(latitude, longitide)))
            .Where(stop => stop.Distance < Constants.JourneySearchRadiusKm)
            .OrderBy(stop => stop.Distance)
            .FirstOrDefault();

        private static bool IsSuitablePoints(Journey journey, JourneyFilter filter)
        {
            var hasSuitablePointsForStart = journey.JourneyPoints.Any(point =>
                CalculateDistance(point, filter.FromLatitude, filter.FromLongitude) < Constants.JourneySearchRadiusKm);
            var hasSuitablePointsForFinish = journey.JourneyPoints.Any(point =>
                CalculateDistance(point, filter.ToLatitude, filter.ToLongitude) < Constants.JourneySearchRadiusKm);

            return hasSuitablePointsForStart && hasSuitablePointsForFinish;
        }

        private static bool IsSuitableSeatsCount(Journey journey, JourneyFilter filter)
        {
            var passengers = journey.JourneyUsers.Sum(journeyUser => journeyUser.PassangersCount);
            return passengers + filter.PassengersCount <= journey.CountOfSeats;
        }
    }
}
