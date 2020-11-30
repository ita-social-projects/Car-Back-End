using System;
using System.Collections.Generic;

namespace Car.DAL.Entities
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public string Location { get; set; }

        public DateTime HireDate { get; set; }

        public string Email { get; set; }

        public string ImageAvatar { get; set; }

        public IEnumerable<Car> UserCars { get; set; }

        public UserPreferences UserPreferences { get; set; }

        public IEnumerable<Notification> UserNotifications { get; set; }

        public Stop UserStop { get; set; }

        public Journey DriverJourney { get; set; }

        public IEnumerable<UserJourney> UserJourneys { get; set; }

        public virtual IEnumerable<Message> SentMessages { get; set; }

        public virtual IEnumerable<Message> ReceivedMessages { get; set; }
    }
}
