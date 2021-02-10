using System;

namespace Car.Data.Entities
{
    public class Notification : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int ReceiverId { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreateAt { get; set; }

        public User User { get; set; }

        public int JourneyId { get; set; }

        public int NotificationTypeId { get; set; }
    }
}
