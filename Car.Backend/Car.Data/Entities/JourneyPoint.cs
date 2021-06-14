namespace Car.Data.Entities
{
    public class JourneyPoint : IEntity
    {
        public int Id { get; set; }

        public int Index { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int JourneyId { get; set; }

        public Journey? Journey { get; set; }
    }
}