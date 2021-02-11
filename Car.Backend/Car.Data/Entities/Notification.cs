using System;

namespace Car.Data.Entities
{
    public class Notification : IEntity
    {
        public int Id { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public int JourneyId { get; set; }

        public NotificationType Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }
    }
}
