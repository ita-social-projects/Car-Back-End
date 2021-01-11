using System;
using System.Collections.Generic;
using System.Text;

namespace Car.DAL.Entities
{
    public class UserChat : IEntity
    {
        public User User { get; set; }

        public int UserId { get; set; }

        public Chat Chat { get; set; }

        public int ChatId { get; set; }
    }
}
