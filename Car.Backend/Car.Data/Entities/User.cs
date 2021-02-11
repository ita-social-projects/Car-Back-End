﻿using System;
using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class User : IEntityWithImage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public DateTime HireDate { get; set; }

        public string Email { get; set; }

        public string ImageId { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public UserPreferences UserPreferences { get; set; }

        public ICollection<Notification> ReceivedNotifications { get; set; } = new List<Notification>();

        public ICollection<Notification> SentNotifications { get; set; } = new List<Notification>();

        public ICollection<Address> Addresses { get; set; } = new List<Address>();

        public ICollection<Journey> OrganizerJourneys { get; set; } = new List<Journey>();

        public ICollection<Journey> ParticipantJourneys { get; set; } = new List<Journey>();

        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
