using System.Collections.Generic;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        public IEnumerable<JourneyModel> GetPastJourneys(int userId);

        public IEnumerable<JourneyModel> GetUpcomingJourneys(int userId);

        public IEnumerable<JourneyModel> GetScheduledJourneys(int userId);

        public JourneyModel GetCurrentJourney(int userId);

        public JourneyModel GetJourneyById(int journeyId);
    }
}
