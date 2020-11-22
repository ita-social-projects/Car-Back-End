using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Entities
{
    class Message : IEntityBase
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
