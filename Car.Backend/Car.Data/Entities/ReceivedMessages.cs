using System.Collections.Generic;

namespace Car.Data.Entities
{
    public class ReceivedMessages : IEntity
    {
        public int UserId { get; set; }

        public int ChatId { get; set; }

        public int UnreadMessagesCount { get; set; }

        public User? User { get; set; }

        public Chat? Chat { get; set; }
    }
}
