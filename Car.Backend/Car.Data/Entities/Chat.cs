using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class Chat : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Journey? Journey { get; set; }

        public ICollection<Message> Messages { get; set; } = new List<Message>();

        public ICollection<ReceivedMessages> ReceivedMessages { get; set; } = new List<ReceivedMessages>();
    }
}