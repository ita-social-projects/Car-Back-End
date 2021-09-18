using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Constants;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Domain.Dto;
using Car.Domain.Dto.Address;
using Car.Domain.Filters;
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

        public static IQueryable<Journey> IncludeJourneyPoints(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.JourneyPoints.OrderBy(point => point.Index));

        public static IQueryable<Journey> IncludeAllParticipants(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Organizer)
                .Include(journey => journey.Participants);

        public static IQueryable<Journey> IncludeJourneyInfo(this IQueryable<Journey> journeys, int userId) =>
            journeys.IncludeAllParticipants().IncludeStopsWithAddresses().FilterByUser(userId);

        public static IQueryable<Journey> IncludeNotifications(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Notifications);

        public static IQueryable<Journey> IncludeSchedule(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Schedule);

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

        public static IQueryable<Journey> SortByDepartureTime(this IQueryable<Journey> journeys) =>
            journeys.OrderBy(journey => journey.DepartureTime);

        public static async Task<IQueryable<Journey>> UseSavedAdresses(this IQueryable<Journey> journeys, ILocationService locationService)
        {
            var savedLocations = await locationService.GetAllByUserIdAsync();
            foreach (var journey in journeys)
            {
                foreach (var stop in journey.Stops)
                {
                    foreach (var location in savedLocations)
                    {
                        if (stop.Address != null && location.Address != null && stop.Address.Name == location.Address.Name
                            && stop.Address.Latitude == location.Address.Latitude
                            && stop.Address.Longitude == location.Address.Longitude)
                        {
                            stop.Address.Name = location.Name;
                        }
                    }
                }
            }

            return journeys;
        }

        public static IQueryable<IEnumerable<StopDto>> SelectStartAndFinishStops(this IQueryable<Journey> journeys)
        {
            var result = journeys.Select(journey => journey.Stops.Select(stop => new StopDto
            {
                Id = stop.Id,
                Type = stop.Type,
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

        public static IEnumerable<Request> FilterUnsuitableRequests(this IQueryable<Request> requests, Journey journey, Func<Request, JourneyFilter> requestToJourneyFilter) =>
            requests.Where(request => journey.Participants.Count + request.PassengersCount <= journey.CountOfSeats)
            .Where(request => journey.DepartureTime > DateTime.UtcNow
                && journey.DepartureTime <= request.DepartureTime.AddHours(Constants.JourneySearchTimeScopeHours)
                && journey.DepartureTime >= request.DepartureTime.AddHours(-Constants.JourneySearchTimeScopeHours))
            .Where(request => (journey.IsFree && request.Fee == FeeType.Free)
                || (!journey.IsFree && request.Fee == FeeType.Paid)
                || (request.Fee == FeeType.All))
            .AsEnumerable()
            .Where(request => IsSuitablePoints(journey, requestToJourneyFilter(request)));

        public static IEnumerable<Journey> FilterUnsuitableJourneys(this IQueryable<Journey> journeys, JourneyFilter filter) =>
            journeys.Where(journey => journey.Participants.Count + filter.PassengersCount <= journey.CountOfSeats)
            .Where(journey => journey.DepartureTime > DateTime.UtcNow
                && journey.DepartureTime <= filter.DepartureTime.AddHours(Constants.JourneySearchTimeScopeHours)
                && journey.DepartureTime >= filter.DepartureTime.AddHours(-Constants.JourneySearchTimeScopeHours))
            .Where(journey => (journey.IsFree && filter.Fee == FeeType.Free)
                || (!journey.IsFree && filter.Fee == FeeType.Paid)
                || (filter.Fee == FeeType.All))
            .AsEnumerable()
            .Where(journey => IsSuitablePoints(journey, filter));

        public static double CalculateDistance(this JourneyPoint point, double latitude, double longitude) =>
            GeoCalculator.GetDistance(
                point.Latitude,
                point.Longitude,
                latitude,
                longitude,
                distanceUnit: DistanceUnit.Kilometers);

        private static bool IsSuitablePoints(Journey journey, JourneyFilter filter)
        {
            var pointsFromStart = journey.JourneyPoints
                .SkipWhile(point => CalculateDistance(point, filter.FromLatitude, filter.FromLongitude) > Constants.JourneySearchRadiusKm);

            return pointsFromStart.Any() && pointsFromStart
                .Any(point => CalculateDistance(point, filter.ToLatitude, filter.ToLongitude) < Constants.JourneySearchRadiusKm);
        }
    }
}
