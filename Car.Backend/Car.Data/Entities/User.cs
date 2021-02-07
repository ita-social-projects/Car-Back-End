using System;
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

        public IEnumerable<Car> Cars { get; set; } = new List<Car>();

        public UserPreferences UserPreferences { get; set; }

        public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();

        public IEnumerable<Address> Addresses { get; set; } = new List<Address>();

        public IEnumerable<Journey> OrganizerJourneys { get; set; } = new List<Journey>();

        public IEnumerable<Journey> ParticipantJourneys { get; set; } = new List<Journey>();

        public IEnumerable<UserJourney> UserJourneys { get; set; } = new List<UserJourney>();

        public virtual IEnumerable<Message> SentMessages { get; set; } = new List<Message>();

        public virtual IEnumerable<Message> ReceivedMessages { get; set; } = new List<Message>();

        public ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
