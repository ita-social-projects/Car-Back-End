using System;

namespace Car.DAL.Entities
{
    public class Notification : IEntity
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Description { get; set; }

        public bool IsRead { get; set; }

        public DateTime CreateAt { get; set; }

        public User User { get; set; }
    }
}
