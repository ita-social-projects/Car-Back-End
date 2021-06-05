using System;
using Car.Data.Enums;

namespace Car.Domain.Filters
{
    public class JourneyFilter
    {
        public int ApplicantId { get; set; }

        public double FromLatitude { get; set; }

        public double FromLongitude { get; set; }

        public double ToLatitude { get; set; }

        public double ToLongitude { get; set; }

        public DateTime DepartureTime { get; set; }

        public bool HasLuggage { get; set; }

        public FeeType Fee { get; set; }

        public int PassengersCount { get; set; }
    }
}
