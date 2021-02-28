using System.Linq;
using Car.Data.Entities;
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
    }
}
