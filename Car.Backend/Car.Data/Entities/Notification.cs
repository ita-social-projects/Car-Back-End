using System;

namespace Car.Data.Entities
{
    public class Notification : IEntity
    {
        public int Id { get; set; }

        public User Sender { get; set; }

        public int ReceiverId { get; set; }

        public NotificationType Type { get; set; }

        public string JsonData { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}