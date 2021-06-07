using System.Collections.Generic;
using Car.Domain.Dto;

namespace Car.Domain.Models.Journey
{
    public class ApplicantJourney
    {
        public JourneyModel Journey { get; set; }

        public IEnumerable<StopDto> ApplicantStops { get; set; }
    }
}
