using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Internal;
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
                .Where(r => IsSuitable(addedJourney, mapper.Map<Request, JourneyFilter>(r)) && addedJourney.OrganizerId != r.UserId)
                .ToList();

            foreach (var request in requests)
            {
                await requestService.NotifyUserAsync(
                    mapper.Map<Request, RequestDto>(request),
                    mapper.Map<Journey, JourneyModel>(addedJourney));
            }

            return mapper.Map<Journey, JourneyModel>(addedJourney);
        }

        public async Task<IEnumerable<JourneyModel>> GetFilteredJourneys(JourneyFilter filter)
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

        public async Task<JourneyModel> UpdateRouteAsync(JourneyDto journeyDto)
        {
            var journey = await journeyRepository.Query()
                    .IncludeStopsWithAddresses()
                    .IncludeJourneyPoints()
                    .FirstOrDefaultAsync(j => j.Id == journeyDto.Id);

            if (journey is null)
            {
                return null;
            }

            var updatedJourney = mapper.Map<JourneyDto, Journey>(journeyDto);

            journey.Duration = updatedJourney.Duration;
            journey.Stops = updatedJourney.Stops;
            journey.JourneyPoints = updatedJourney.JourneyPoints;
            journey.RouteDistance = updatedJourney.RouteDistance;

            await journeyRepository.SaveChangesAsync();

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task<JourneyModel> UpdateDetailsAsync(JourneyDto journeyDto)
        {
            var journey = mapper.Map<JourneyDto, Journey>(journeyDto);

            journey = await journeyRepository.UpdateAsync(journey);
            await journeyRepository.SaveChangesAsync();

            return mapper.Map<Journey, JourneyModel>(journey);
        }

        public async Task<IEnumerable<ApplicantJourney>> GetApplicantJourneys(JourneyFilter filter)
        {
            var journeysResult = new List<ApplicantJourney>();

            var filteredJourneys = await GetFilteredJourneys(filter);

            foreach (var journey in filteredJourneys)
            {
                journeysResult.Add(new ApplicantJourney()
                {
                    Journey = journey,
                    ApplicantStops = GetApplicantStops(filter, journey),
                });
            }

            return journeysResult;
        }

        public IEnumerable<StopDto> GetApplicantStops(JourneyFilter filter, JourneyModel journey)
        {
            var applicantStops = new List<StopDto>();

            var distances = journey.JourneyPoints.Select(p => CalculateDistance(
                mapper.Map<JourneyPointDto, JourneyPoint>(p),
                filter.FromLatitude,
                filter.FromLongitude)).ToList();

            var pointIndex = distances.IndexOf(distances.Min());
            var startRoutePoint = journey.JourneyPoints.ToList()[pointIndex];

            distances = journey.JourneyPoints.ToList()
                .GetRange(pointIndex, journey.JourneyPoints.Count - pointIndex)
                .Select(p => CalculateDistance(
                    mapper.Map<JourneyPointDto, JourneyPoint>(p),
                    filter.ToLatitude,
                    filter.ToLongitude)).ToList();

            var endRoutePoint = journey.JourneyPoints.ToList()[distances.IndexOf(distances.Min())];

            applicantStops.Add(new StopDto()
            {
                Id = 0,
                Index = 0,
                UserId = filter.ApplicantId,
                Address = new AddressDto() { Latitude = startRoutePoint.Latitude, Longitude = startRoutePoint.Longitude },
                Type = StopType.Intermediate,
            });

            applicantStops.Add(new StopDto()
            {
                Id = 0,
                Index = 1,
                UserId = filter.ApplicantId,
                Address = new AddressDto() { Latitude = endRoutePoint.Latitude, Longitude = endRoutePoint.Longitude },
                Type = StopType.Intermediate,
            });

            return applicantStops;
        }

        private static bool IsSuitable(Journey journey, JourneyFilter filter)
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
                .SkipWhile(point => CalculateDistance(point, filter.FromLatitude, filter.FromLongitude) > Constants.JourneySearchRadiusKm);

            return pointsFromStart.Any() && pointsFromStart
                .Any(point => CalculateDistance(point, filter.ToLatitude, filter.ToLongitude) < Constants.JourneySearchRadiusKm);
        }

        private static double CalculateDistance(JourneyPoint point, double latitude, double longitude) =>
            GeoCalculator.GetDistance(
                point.Latitude,
                point.Longitude,
                latitude,
                longitude,
                distanceUnit: DistanceUnit.Kilometers);
    }
}
