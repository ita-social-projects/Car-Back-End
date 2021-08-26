using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car.Data.Entities
{
    public class User : IEntityWithImage
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Surname { get; set; } = string.Empty;

        public string? Position { get; set; }

        public string? Location { get; set; }

        public DateTime HireDate { get; set; }

        public string Email { get; set; } = string.Empty;

        public string? ImageId { get; set; }

        public int BadgeCount { get; set; }

        public int JourneyCount { get; set; }

        [NotMapped]
        public string Token { get; set; } = string.Empty;

        public string? FCMToken { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public UserPreferences? UserPreferences { get; set; }

        public ICollection<Notification> ReceivedNotifications { get; set; } = new List<Notification>();

        public ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();

        public ICollection<Location> Locations { get; set; } = new List<Location>();

        public ICollection<Stop> Stops { get; set; } = new List<Stop>();

        public ICollection<Journey> OrganizerJourneys { get; set; } = new List<Journey>();

        public ICollection<Journey> ParticipantJourneys { get; set; } = new List<Journey>();

        public ICollection<JourneyUser> JourneyUsers { get; set; } = new List<JourneyUser>();

        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        public ICollection<Request> Requests { get; set; } = new List<Request>();
    }
}
