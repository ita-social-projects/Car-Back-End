using System;

namespace Car.DAL.Entities
{
    public class Message : IEntity
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime CreateAt { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        public virtual User Sender { get; set; }

        public virtual User Receiver { get; set; }
    }
}
