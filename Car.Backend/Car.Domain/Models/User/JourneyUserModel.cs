namespace Car.Domain.Models.User
{
    public class JourneyUserModel
    {
        public int JourneyId { get; set; }

        public int UserId { get; set; }

        public bool WithBaggage { get; set; }

        public int PassangersCount { get; set; }
    }
}