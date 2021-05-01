using System;
using System.Collections.Generic;
using Car.Domain.Dto;

namespace Car.Domain.Models.Journey
{
    public class CreateJourneyModel
    {
        public DateTime DepartureTime { get; set; }

        public int CountOfSeats { get; set; }

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public int OrganizerId { get; set; }

        public int CarId { get; set; }

        public bool IsOnOwnCar { get; set; }

        // public ICollection<Coordinates> RoutePoints { get; set; } = new List<Coordinates>();

        public ICollection<StopDto> Stops { get; set; } = new List<StopDto>();
    }
}