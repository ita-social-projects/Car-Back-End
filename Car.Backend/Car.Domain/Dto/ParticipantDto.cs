namespace Car.Domain.Dto
{
    public sealed class ParticipantDto
    {
        public int JourneyId { get; set; }

        public int UserId { get; set; }

        public bool HasLuggage { get; set; }
    }
}