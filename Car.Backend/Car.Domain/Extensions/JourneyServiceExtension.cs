using System;
using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Extensions
{
    public static class JourneyServiceExtension
    {
        public static IQueryable<Journey> FilterByUser(this IQueryable<Journey> journeys, int userId) =>
            journeys.Where(journey => journey.Participants.Any(user => user.Id == userId) || journey.OrganizerId == userId);

        public static IQueryable<Journey> IncludeStopsWithAddresses(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Stops.OrderBy(stop => stop.Type))
                .ThenInclude(stop => stop.Address);

        public static IQueryable<Journey> IncludeAllParticipants(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.Organizer)
                .Include(journey => journey.Participants);

        public static IQueryable<Journey> IncludeJourneyInfo(this IQueryable<Journey> journeys, int userId) =>
            journeys.IncludeAllParticipants().IncludeStopsWithAddresses().FilterByUser(userId);

        public static IQueryable<Journey> IncludeJourneyPoints(this IQueryable<Journey> journeys) =>
            journeys.Include(journey => journey.JourneyPoints);

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

        public static IQueryable<IEnumerable<StopDto>> SelectStartAndFinishStops(this IQueryable<Journey> journeys)
        {
            var result = journeys.Select(journey => journey.Stops.Select(stop => new StopDto
            {
                Id = stop.Id,
                Type = stop.Type,
                Address = new AddressDto
                {
                    Id = stop.Address.Id,
                    Name = stop.Address.Name,
                    Longitude = stop.Address.Longitude,
                    Latitude = stop.Address.Latitude,
                },
            }));

            return result;
        }
    }
}
