using System;
using System.Collections.Generic;
using Car.Data.Enums;

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

        public WeekDays? WeekDay { get; set; }

        public ICollection<StopDto> Stops { get; set; } = new List<StopDto>();

        public ICollection<InvitationDto> Invitations { get; set; } = new List<InvitationDto>();

        public ICollection<JourneyPointDto> JourneyPoints { get; set; } = new List<JourneyPointDto>();
    }
}