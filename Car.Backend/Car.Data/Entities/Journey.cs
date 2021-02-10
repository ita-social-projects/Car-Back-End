using System;
using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Journey : IEntity
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan JourneyDuration { get; set; }

        public int CountOfSeats { get; set; }

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public int? OrganizerId { get; set; }

        public int? ScheduleId { get; set; }

        public ICollection<User> Participants { get; set; } = new List<User>();

        public ICollection<UserJourney> UserJourneys { get; set; } = new List<UserJourney>();

        public IEnumerable<Stop> Stops { get; set; } = new List<Stop>();

        public Schedule Schedule { get; set; }

        public User Organizer { get; set; }
    }
}
