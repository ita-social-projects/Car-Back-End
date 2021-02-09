using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public User Reciver { get; set; }

        public User User { get; set; }
    }
}