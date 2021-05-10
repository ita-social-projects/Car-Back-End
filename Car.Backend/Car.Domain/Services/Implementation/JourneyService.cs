using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Geolocation;
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
            var journeys = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
                .ToListAsync();

            return mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys.Where(j => IsSuitable(j, filter)));
        }

        private bool IsSuitable(Journey journey, JourneyFilterModel filter)
        {
            bool isEnoughSeats = journey.Participants.Count + filter.PassengersCount <= journey.CountOfSeats;

            bool isDepartureTimeSuitable = journey.DepartureTime < filter.DepartureTime.AddHours(1)
                                       && journey.DepartureTime > filter.DepartureTime.AddHours(-1);

            bool isFeeSuitable = (journey.IsFree && filter.Fee == FeeType.Free)
                              || (!journey.IsFree && filter.Fee == FeeType.Paid)
                              || (filter.Fee == FeeType.All);

            if (!isEnoughSeats || !isDepartureTimeSuitable || !isFeeSuitable)
            {
                return false;
            }

            Func<JourneyPoint, AddressDto, double> distance = (JourneyPoint address1, AddressDto address2) =>
                GeoCalculator.GetDistance(address1.Latitude, address2.Longitude, address2.Latitude, address2.Longitude, 1, DistanceUnit.Kilometers);

            var pointsFromStart = journey.JourneyPoints.OrderBy(p => p.Index).SkipWhile(p => distance(p, filter.FromStop.Address) < 1);

            return pointsFromStart.Count() > 0 && pointsFromStart.Any(p => distance(p, filter.ToStop.Address) < 1);
        }
    }
}
