﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
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

        public async Task<IEnumerable<JourneyModel>> GetPastJourneysAsync(int userId)
        {
            var journeys = await journeyRepository
                .Query()
                .IncludeJourneyInfo(userId)
                .FilterPast()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetScheduledJourneysAsync(int userId)
        {
            var journeys = await journeyRepository
                .Query(journey => journey.Schedule)
                .IncludeJourneyInfo(userId)
                .Where(journey => journey.Schedule != null)
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<IEnumerable<JourneyModel>> GetUpcomingJourneysAsync(int userId)
        {
            var journeys = await journeyRepository
                .Query()
                .IncludeJourneyInfo(userId)
                .FilterUpcoming()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public async Task<List<IEnumerable<StopDto>>> GetStopsFromRecentJourneysAsync(int userId, int countToTake = 5)
        {
            var journeys = await journeyRepository.Query()
                .IncludeStopsWithAddresses()
                .FilterByUser(userId)
                .OrderByDescending(journey => journey.DepartureTime)
                .Take(countToTake)
                .SelectStartAndFinishStops()
                .ToListAsync();

            return journeys;
        }

        public async Task DeletePastJourneyAsync()
        {
            var now = DateTime.Now;
            var termInDays = 14;

            var journeysToDelete = journeyRepository
                .Query()
                .AsEnumerable()
                .Where(j => (now - j.DepartureTime).TotalDays >= termInDays)
                .ToList();

            await journeyRepository.DeleteRangeAsync(journeysToDelete);
            await journeyRepository.SaveChangesAsync();
        }

        public async Task<JourneyModel> AddJourneyAsync(CreateJourneyModel journeyModel)
        {
            journeyModel.Stops.ForAll(stop => stop.UserId = journeyModel.OrganizerId);

            var journey = mapper.Map<CreateJourneyModel, Journey>(journeyModel);

            var addedJourney = await journeyRepository.AddAsync(journey);
            await journeyRepository.SaveChangesAsync();

            return mapper.Map<Journey, JourneyModel>(addedJourney);
        }

        public async Task<IEnumerable<JourneyModel>> GetFilteredJourneys(JourneyFilterModel filter)
        {
            // filtering algorithm implementation is in progress
            var journeys = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }
    }
}
