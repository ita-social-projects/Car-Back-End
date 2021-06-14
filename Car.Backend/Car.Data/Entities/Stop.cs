using Car.Data.Enums;

namespace Car.Data.Entities
{
    public class Stop : IEntity
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public int JourneyId { get; set; }

        public int AddressId { get; set; }

        public int UserId { get; set; }

        public StopType Type { get; set; }

        public Journey? Journey { get; set; }

        public Address? Address { get; set; }

        public User? User { get; set; }
    }
}
