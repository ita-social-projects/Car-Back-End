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

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public IEnumerable<UserDto> Participants { get; set; } = new List<UserDto>();

        public IEnumerable<StopDto> Stops { get; set; } = new List<StopDto>();

        public UserDto Organizer { get; set; }
    }
}
