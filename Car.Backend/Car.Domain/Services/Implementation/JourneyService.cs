using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Constants;
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
        private readonly IRepository<Request> requestRepository;
        private readonly IRequestService requestService;
        private readonly IMapper mapper;

        public JourneyService(
            IRepository<Journey> journeyRepository,
            IRepository<Request> requestRepository,
            IRequestService requestService,
            IMapper mapper)
        {
            this.journeyRepository = journeyRepository;
            this.requestRepository = requestRepository;
            this.requestService = requestService;
            this.mapper = mapper;
        }

        public async Task<JourneyModel> GetJourneyByIdAsync(int journeyId)
        {
            var journey = await journeyRepository
                .Query()
                .IncludeAllParticipants()
                .IncludeStopsWithAddresses()
                .IncludeJourneyPoints()
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

        public async Task<JourneyModel> AddJourneyAsync(JourneyDto journeyModel)
        {
            var journey = mapper.Map<JourneyDto, Journey>(journeyModel);

            var addedJourney = await journeyRepository.AddAsync(journey);
            await journeyRepository.SaveChangesAsync();

            var requests = requestRepository
                .Query()
                .AsEnumerable()
                .Where(r => IsSuitable(addedJourney, mapper.Map<Request, JourneyFilterModel>(r)))
                .ToList();

            foreach (var request in requests)
            {
                await requestService.NotifyUserAsync(
                    mapper.Map<Request, RequestDto>(request),
                    mapper.Map<Journey, JourneyModel>(addedJourney));
            }

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

        public async Task DeleteAsync(int journeyId)
        {
            journeyRepository.Delete(new Journey { Id = journeyId });
            await journeyRepository.SaveChangesAsync();
        }

        public async Task<JourneyModel> UpdateAsync(JourneyDto journeyDto)
        {
            var journey = mapper.Map<JourneyDto, Journey>(journeyDto);

            var updatedJourney = await journeyRepository.UpdateAsync(journey);
            await journeyRepository.SaveChangesAsync();

            return mapper.Map<Journey, JourneyModel>(updatedJourney);
        }

        private static bool IsSuitable(Journey journey, JourneyFilterModel filter)
        {
            var isEnoughSeats = journey.Participants.Count + filter.PassengersCount <= journey.CountOfSeats;

            var isDepartureTimeSuitable = journey.DepartureTime > DateTime.UtcNow
                                          && journey.DepartureTime <= filter.DepartureTime.AddHours(Constants.JourneySearchTimeScopeHours)
                                          && journey.DepartureTime >= filter.DepartureTime.AddHours(-Constants.JourneySearchTimeScopeHours);

            var isFeeSuitable = (journey.IsFree && filter.Fee == FeeType.Free)
                                || (!journey.IsFree && filter.Fee == FeeType.Paid)
                                || (filter.Fee == FeeType.All);

            if (!isEnoughSeats || !isDepartureTimeSuitable || !isFeeSuitable)
            {
                return false;
            }

            var pointsFromStart = journey.JourneyPoints
                .SkipWhile(point => Distance(point, filter.FromLatitude, filter.FromLongitude) > Constants.JourneySearchRadiusKm);

            return pointsFromStart.Any() && pointsFromStart
                .Any(point => Distance(point, filter.ToLatitude, filter.ToLongitude) < Constants.JourneySearchRadiusKm);

            static double Distance(JourneyPoint point, double latitude, double longitude) =>
                GeoCalculator.GetDistance(
                    point.Latitude,
                    point.Longitude,
                    latitude,
                    longitude,
                    distanceUnit: DistanceUnit.Kilometers);
        }
    }
}
