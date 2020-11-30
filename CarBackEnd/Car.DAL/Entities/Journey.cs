using System;
using System.Collections.Generic;

namespace Car.DAL.Entities
{
    public class Journey : IEntity
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public int CountOfSeats { get; set; }

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public int? DriverId { get; set; }

        public int? ScheduleId { get; set; }

        public IEnumerable<UserJourney> Participents { get; set; }

        public IEnumerable<Stop> UserStops { get; set; }

        public Schedule Schedule { get; set; }

        public User Driver { get; set; }
    }
}
