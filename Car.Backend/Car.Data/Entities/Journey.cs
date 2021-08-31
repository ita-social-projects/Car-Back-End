using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car.Data.Entities
{
    public class Journey : IEntity
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int CountOfSeats { get; set; }

        public string? Comments { get; set; }

        public bool IsFree { get; set; }

        public bool IsCancelled { get; set; }

        public bool IsOnOwnCar { get; set; }

        public int OrganizerId { get; set; }

        public int? CarId { get; set; }

        public int? ParentId { get; set; }

        [NotMapped]
        public DateTime EndTime => DepartureTime.Add(Duration);

        public Car? Car { get; set; }

        public Schedule? Schedule { get; set; }

        public Schedule? Parent { get; set; }

        public User? Organizer { get; set; }

        public Chat? Chat { get; set; }

        public ICollection<User> Participants { get; set; } = new List<User>();

        public ICollection<JourneyUser> JourneyUsers { get; set; } = new List<JourneyUser>();

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();

        public ICollection<JourneyPoint> JourneyPoints { get; set; } = new List<JourneyPoint>();

        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}
