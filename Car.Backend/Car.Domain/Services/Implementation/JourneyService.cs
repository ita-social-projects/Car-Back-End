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
