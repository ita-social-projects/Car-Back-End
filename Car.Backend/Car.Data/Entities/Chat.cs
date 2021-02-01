using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<UserChat> UserChats { get; set; }

        public IEnumerable<User> Members { get; set; }
    }
}
