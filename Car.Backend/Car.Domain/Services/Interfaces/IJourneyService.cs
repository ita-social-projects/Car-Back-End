using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        public List<Journey> GetPastJourneys(int userId);

        public List<Journey> GetUpcomingJourneys(int userId);

        public List<Journey> GetScheduledJourneys(int userId);

        public Journey GetCurrentJourney(int userId);

        public Task AddParticipantAsync(ParticipantDto participantDto);

        public Journey GetJourneyById(int journeyId);
    }
}
