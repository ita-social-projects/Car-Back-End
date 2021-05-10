using System;
using Car.Data.Enums;
using Car.Domain.Dto;

namespace Car.Domain.Models.Journey
{
    public class JourneyFilterModel
    {
        public StopDto FromStop { get; set; }

        public StopDto ToStop { get; set; }

        public DateTime DepartureTime { get; set; }

        public bool HasLuggage { get; set; }

        public FeeType Fee { get; set; }

        public int PassengersCount { get; set; }
    }
}
