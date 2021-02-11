﻿using System;

namespace Car.Data.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int SenderId { get; set; }

        public int ChatId { get; set; }

        public virtual User Sender { get; set; }

        public virtual Chat Chat { get; set; }
    }
}
