using System;
using System.Collections.Generic;

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

        public ICollection<CreateJourneyPointModel> JourneyPoints { get; set; } = new List<CreateJourneyPointModel>();

        public ICollection<CreateStopModel> Stops { get; set; } = new List<CreateStopModel>();
    }
}