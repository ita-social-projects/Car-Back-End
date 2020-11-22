
namespace Car.DAL.Entities
{
    class Stop
    {
        public int Id { get; set; }
        public int JourneyId { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }

        public Journey Journey { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
    }
}
