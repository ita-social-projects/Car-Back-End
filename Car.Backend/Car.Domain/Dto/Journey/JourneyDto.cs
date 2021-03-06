using System;
using System.Collections.Generic;

namespace Car.Domain.Dto
{
    public class JourneyDto
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int CountOfSeats { get; set; }

        public string? Comments { get; set; }

        public bool IsFree { get; set; }

        public bool IsOnOwnCar { get; set; }

        public bool IsCancelled { get; set; }

        public int OrganizerId { get; set; }

        public int? CarId { get; set; }

        public ICollection<StopDto> Stops { get; set; } = new List<StopDto>();

        public ICollection<JourneyPointDto> JourneyPoints { get; set; } = new List<JourneyPointDto>();
    }
}