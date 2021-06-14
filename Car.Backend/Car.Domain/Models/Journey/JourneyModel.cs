using System;
using System.Collections.Generic;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Models.Journey
{
    public class JourneyModel
    {
        public int Id { get; set; }

        public int RouteDistance { get; set; }

        public DateTime DepartureTime { get; set; }

        public TimeSpan Duration { get; set; }

        public int CountOfSeats { get; set; }

        public string? Comments { get; set; }

        public bool IsFree { get; set; }

        public bool IsOnOwnCar { get; set; }

        public Schedule? Schedule { get; set; }

        public UserDto? Organizer { get; set; }

        public Data.Entities.Car? Car { get; set; }

        public ICollection<UserDto> Participants { get; set; } = new List<UserDto>();

        public ICollection<JourneyPointDto> JourneyPoints { get; set; } = new List<JourneyPointDto>();

        public ICollection<StopDto> Stops { get; set; } = new List<StopDto>();
    }
}
