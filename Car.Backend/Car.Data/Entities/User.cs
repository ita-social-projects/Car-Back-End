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

        public IEnumerable<Car> UserCars { get; set; }

        public UserPreferences UserPreferences { get; set; }

        public IEnumerable<Notification> UserNotifications { get; set; }

        public Stop UserStop { get; set; }

        public IEnumerable<Journey> DriverJourney { get; set; }

        public IEnumerable<UserJourney> UserJourneys { get; set; }

        public virtual IEnumerable<Message> SentMessages { get; set; }

        public virtual IEnumerable<Message> ReceivedMessages { get; set; }

        public ICollection<Chat> Chats { get; set; }
    }
}
