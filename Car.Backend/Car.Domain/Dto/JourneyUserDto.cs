namespace Car.Domain.Dto
{
    public sealed class JourneyUserDto
    {
        public int JourneyId { get; set; }

        public int UserId { get; set; }

        public bool WithBaggage { get; set; }
    }
}

