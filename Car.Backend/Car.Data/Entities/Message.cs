using System;

namespace Car.Data.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public int SenderId { get; set; }

        public int ChatId { get; set; }

        public User? Sender { get; set; }

        public Chat? Chat { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
