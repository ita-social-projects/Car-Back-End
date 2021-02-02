using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string ChatName { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
