using System;
using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Journey : IEntity
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int CountOfSeats { get; set; }

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public int OrganizerId { get; set; }

        public Schedule Schedule { get; set; }

        public User Organizer { get; set; }

        public Chat Chat { get; set; }

        public ICollection<User> Participants { get; set; } = new List<User>();

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
