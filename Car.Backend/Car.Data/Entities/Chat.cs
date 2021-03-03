using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Journey Journey { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}