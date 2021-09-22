using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Extensions
{
    public static class ScheduleServiceExtensions
    {
        public static IQueryable<Schedule> IncludeJourney(this IQueryable<Schedule> schedules) =>
            schedules.Include(schedule => schedule.Journey);

        public static IQueryable<Schedule> IncludeChildJourneys(this IQueryable<Schedule> schedules) =>
            schedules.Include(schedule => schedule.ChildJourneys);

        public static IQueryable<Schedule> IncludeJourneyWithRouteInfo(this IQueryable<Schedule> schedules) =>
            schedules
                .Include(schedule => schedule.Journey)
                .ThenInclude(journey => journey!.JourneyPoints)
                .Include(schedule => schedule.Journey)
                .ThenInclude(journey => journey!.Stops)
                .ThenInclude(stop => stop.Address);
    }
}
