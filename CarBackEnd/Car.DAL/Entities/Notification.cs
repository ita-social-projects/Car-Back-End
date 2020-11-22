using System;

namespace Car.DAL.Entities
{
    class Notification : IEntityBase
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreateTime { get; set; }

        public User User { get; set; }
    }
}
