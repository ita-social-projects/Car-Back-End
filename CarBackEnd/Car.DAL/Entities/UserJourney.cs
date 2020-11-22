
namespace Car.DAL.Entities
{
    class UserJourney
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int JourneyId { get; set; }
        public Journey Journey { get; set; }
    }
}
