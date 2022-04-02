using Car.Data.Entities;

namespace Car.Domain.Models.Common
{
    public class MergeStop
    {
        public Stop Stop { get; set; }

        public double Distance { get; set; }

        public MergeStop(Stop stop, double distance)
        {
            Stop = stop;
            Distance = distance;
        }
    }
}
