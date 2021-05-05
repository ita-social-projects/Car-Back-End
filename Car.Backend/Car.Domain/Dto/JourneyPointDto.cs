namespace Car.Domain.Dto
{
    public class JourneyPointDto
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int JourneyId { get; set; }
    }
}