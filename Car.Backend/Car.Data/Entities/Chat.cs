using Car.Data.Entities;
using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string ChatName { get; set; }

        public IEnumerable<UserChat> Users { get; set; }
    }
}
