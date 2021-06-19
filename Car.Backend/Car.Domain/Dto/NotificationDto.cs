using System;
using Car.Data.Entities;

namespace Car.Domain.Dto
{
    public class NotificationDto
    {
        public int Id { get; set; }

        public User Sender { get; set; }

        public User Receiver { get; set; }

        public int ReceiverId { get; set; }

        public int SenderId { get; set; }

        public NotificationType Type { get; set; }

        public string JsonData { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}