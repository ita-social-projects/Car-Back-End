using System.Collections.Generic;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        public IEnumerable<JourneyDto> GetPastJourneys(int userId);

        public IEnumerable<JourneyDto> GetUpcomingJourneys(int userId);

        public IEnumerable<JourneyDto> GetScheduledJourneys(int userId);

        public JourneyDto GetCurrentJourney(int userId);

        public JourneyDto GetJourneyById(int journeyId);
    }
}
