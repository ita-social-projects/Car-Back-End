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

        public Task PostApproveApplicantAsync(Int32 journeyId, Int32 userId, Boolean hasLuggage = false);
    }
}
