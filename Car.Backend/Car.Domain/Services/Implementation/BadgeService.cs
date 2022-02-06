using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Hubs;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class BadgeService : IBadgeService
    {
        private readonly IRepository<UserStatistic> userStatisticRepository;
        private readonly IRepository<User> userRepository;
        private readonly IJourneyService journeyService;
        private readonly IHubContext<SignalRHub> hub;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRepository<Journey> journeyRepository;

        public BadgeService(
            IRepository<UserStatistic> userStatisticRepository,
            IRepository<User> userRepository,
            IRepository<Journey> journeyRepository,
            IJourneyService journeyService,
            IHubContext<SignalRHub> hub,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userStatisticRepository = userStatisticRepository;
            this.userRepository = userRepository;
            this.journeyRepository = journeyRepository;
            this.journeyService = journeyService;
            this.hub = hub;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task UpdateStatisticsAsync()
        {
            var journeys = await journeyService.GetUncheckedJourneysAsync();

            foreach (var journey in journeys)
            {
                var passangerStatistics = new List<UserStatistic>();
                var organizer = journey.Organizer!;
                var organizerStat = await GetUserStatisticByUserIdAsync(organizer.Id);
                var passangers = journey.Participants;

                if (journey.IsOnOwnCar)
                {
                    organizerStat.DriverJourneysAmount += 1;
                    organizerStat.TotalKm += journey.RouteDistance;
                }
                else
                {
                    organizerStat.PassangerJourneysAmount += 1;
                }

                foreach (var passanger in passangers)
                {
                    var passangerStat = await GetUserStatisticByUserIdAsync(passanger.Id);

                    passangerStat.PassangerJourneysAmount += 1;

                    passangerStatistics.Add(passangerStat);
                }

                journey.IsMarkedAsFinished = true;
                await journeyRepository.UpdateAsync(journey);
                await userStatisticRepository.UpdateAsync(organizerStat);
                await userStatisticRepository.UpdateRangeAsync(passangerStatistics);
                await userStatisticRepository.SaveChangesAsync();
                await hub.Clients.User(organizer.Email).SendAsync("RecieveStats", organizer);
                foreach (var passanger in passangers)
                {
                    await hub.Clients.User(passanger.Email).SendAsync(
                        "RecieveStats",
                        GetUserStatisticByUserIdAsync(passanger.Id));
                }
            }
        }

        public async Task<UserStatistic> GetUserStatisticByUserIdAsync(int userId)
        {
            return await userStatisticRepository.Query()
                .Where(user => user.Id == userId).FirstOrDefaultAsync();
        }

        public async Task<UserStatistic> GetUserStatistic()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            var userStatistic = await GetUserStatisticByUserIdAsync(userId);

            return userStatistic;
        }
    }
}