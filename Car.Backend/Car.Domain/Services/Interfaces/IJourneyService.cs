using System.Collections.Generic;
using Car.Domain.Dto;
using Car.Domain.Models;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        public IEnumerable<JourneyModel> GetPastJourneys(int userId);

        public IEnumerable<JourneyModel> GetUpcomingJourneys(int userId);

        public IEnumerable<JourneyModel> GetScheduledJourneys(int userId);

        public JourneyModel GetJourneyById(int journeyId);

        public List<List<StopDto>> GetStopsFromRecentJourneys(int userId, int countToTake = 5);
    }
}
