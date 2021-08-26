using System;
using System.Collections.Generic;
using Car.Domain.Dto;

namespace Car.Domain.Models.Journey
{
    public class JourneyApplyModel
    {
        public JourneyUserDto? JourneyUser { get; set; }

        public IEnumerable<StopDto> ApplicantStops { get; set; } = new List<StopDto>();
    }
}
