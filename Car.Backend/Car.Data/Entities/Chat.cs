using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
