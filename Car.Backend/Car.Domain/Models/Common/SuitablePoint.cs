using Car.Data.Entities;

namespace Car.Domain.Models.Common
{
    public class SuitablePoint
    {
        public JourneyPoint JourneyPoint { get; set; }

        public double Distance { get; set; }

        public SuitablePoint(JourneyPoint journeyPoint, double distance)
        {
            JourneyPoint = journeyPoint;
            Distance = distance;
        }
    }
}
