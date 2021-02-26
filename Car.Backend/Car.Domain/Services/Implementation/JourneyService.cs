using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Extensions;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IRepository<Journey> journeyRepository;
        private readonly IMapper mapper;

        public JourneyService(IRepository<Journey> journeyRepository, IMapper mapper)
        {
            this.journeyRepository = journeyRepository;
            this.mapper = mapper;
        }

        public async Task<JourneyModel> GetJourneyByIdAsync(int journeyId)
        {
            var journey = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .FirstOrDefaultAsync(j => j.Id == journeyId);

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task<List<JourneyModel>> GetPastJourneysAsync(int userId)
        {
            var now = DateTime.UtcNow;

            var journeys = await journeyRepository
                .Query()
                .IncludeJourneyInfo(userId)
                .Where(journey =>
                    EF.Functions.DateDiffMinute(now, journey.DepartureTime) > journey.Duration.TotalMinutes)
                .ToListAsync();

            return mapper.Map<List<Journey>, List<JourneyModel>>(journeys);
        }

        public async Task<List<JourneyModel>> GetScheduledJourneysAsync(int userId)
        {
            var journeys = await journeyRepository
                .Query(journey => journey.Schedule)
                .IncludeJourneyInfo(userId)
                .Where(journey => journey.Schedule != null)
                .ToListAsync();

            return mapper.Map<List<Journey>, List<JourneyModel>>(journeys);
        }

        public async Task<List<JourneyModel>> GetUpcomingJourneysAsync(int userId)
        {
            var now = DateTime.UtcNow;

            var journeys = await journeyRepository
                .Query()
                .IncludeJourneyInfo(userId)
                .Where(journey => journey.DepartureTime > now)
                .ToListAsync();

            return mapper.Map<List<Journey>, List<JourneyModel>>(journeys);
        }
    }
}
