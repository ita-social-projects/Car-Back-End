using System.Collections.Generic;
using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Stop : IEntity
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public bool IsCancelled { get; set; }

        public int JourneyId { get; set; }

        public int AddressId { get; set; }

        public Journey? Journey { get; set; }

        public Address? Address { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public ICollection<UserStop> UserStops { get; set; } = new List<UserStop>();
    }
}
