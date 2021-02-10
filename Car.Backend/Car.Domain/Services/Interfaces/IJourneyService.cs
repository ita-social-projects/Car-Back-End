using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        public List<Journey> GetPastJourneys(int userId);

        public List<Journey> GetUpcomingJourneys(int userId);

        public List<Journey> GetScheduledJourneys(int userId);

        public Journey GetCurrentJourney(int userId);

        public Task PostApproveApplicantAsync(int journeyId, int userId, bool hasLuggage = false);
    }
}
