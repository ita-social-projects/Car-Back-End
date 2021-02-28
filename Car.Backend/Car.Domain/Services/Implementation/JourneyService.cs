using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Models;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork<Journey> journeyUnitOfWork;

        private readonly IMapper mapper;

        public JourneyService(IUnitOfWork<Journey> journeyUnitOfWork, IMapper mapper)
        {
            this.journeyUnitOfWork = journeyUnitOfWork;
            this.mapper = mapper;
        }

        public JourneyModel GetJourneyById(int journeyId)
        {
            var journey = journeyUnitOfWork.GetRepository()
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .FirstOrDefault(j => j.Id == journeyId);

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public IEnumerable<JourneyModel> GetPastJourneys(int userId)
        {
            var now = DateTime.UtcNow;

            var journeys = journeyUnitOfWork.GetRepository()
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .FilterByUser(userId)
                .AsEnumerable()
                .Where(journey => (journey.DepartureTime + journey.Duration) < now);

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public IEnumerable<JourneyModel> GetScheduledJourneys(int userId)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query(journey => journey.Schedule)
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .FilterByUser(userId)
                .Where(journey => journey.Schedule != null)
                .Select(journey => mapper.Map<Journey, JourneyModel>(journey));

            return journeys;
        }

        public IEnumerable<JourneyModel> GetUpcomingJourneys(int userId)
        {
            var now = DateTime.UtcNow;

            var journeys = journeyUnitOfWork.GetRepository()
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .FilterByUser(userId)
                .Where(journey => journey.DepartureTime > now);

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);
        }

        public List<List<StopDto>> GetStopsFromRecentJourneys(int userId, int countToTake = 5)
        {
            var journeys = journeyUnitOfWork.GetRepository()
                .Query().Include(journey => journey.Stops)
                .ThenInclude(stop => stop.Address)
                .Where(journey => journey.Participants
                    .Any(user => user.Id == userId))
                .OrderByDescending(journey => journey.DepartureTime)
                .Take(countToTake)
                .Select(journeyStops => journeyStops.Stops
                                        .Select(stop => new StopDto
                                        {
                                            Id = stop.Id,
                                            Type = stop.Type,
                                            Address = new AddressDto
                                            {
                                                Id = stop.Address.Id,
                                                City = stop.Address.City,
                                                Street = stop.Address.Street,
                                                Longitude = stop.Address.Longitude,
                                                Latitude = stop.Address.Latitude,
                                            },
                                        }).ToList())
                .ToList();

            return journeys;
        }
    }
}
