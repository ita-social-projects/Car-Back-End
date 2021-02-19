using System;
using System.Collections.Generic;
using Car.Domain.Dto;

namespace Car.Domain.Models
{
    public class JourneyModel
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int CountOfSeats { get; set; }

        public string Comments { get; set; }

        public bool IsFree { get; set; }

        public ICollection<UserDto> Participants { get; set; } = new List<UserDto>();

        public ICollection<StopDto> Stops { get; set; } = new List<StopDto>();

        public UserDto Organizer { get; set; }

        public Data.Entities.Car Car { get; set; }
    }
}
