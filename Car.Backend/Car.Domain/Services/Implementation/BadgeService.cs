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
                var statistics = new List<UserStatistic>();
                var organizerStat = await GetUserStatisticByUserIdAsync(journey.OrganizerId);
                var passangers = journey.Participants.Select(p => p.Id);

                if (journey.IsOnOwnCar)
                {
                    organizerStat.DriverJourneysAmount += 1;
                    organizerStat.TotalKm += journey.RouteDistance;
                }
                else
                {
                    organizerStat.PassangerJourneysAmount += 1;
                }

                statistics.Add(organizerStat);

                foreach (var passanger in passangers)
                {
                    var passangerStat = await GetUserStatisticByUserIdAsync(passanger);

                    passangerStat.PassangerJourneysAmount += 1;

                    statistics.Add(passangerStat);
                }

                journey.IsMarkedAsFinished = true;
                await journeyRepository.UpdateAsync(journey);
                await userStatisticRepository.UpdateRangeAsync(statistics);
                await userStatisticRepository.SaveChangesAsync();

                foreach (var user in statistics)
                {
                    await SendStatisticToUser(user);
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

        private async Task SendStatisticToUser(UserStatistic userStat)
        {
            await hub.Clients.Group($"{userStat.Id}")
                .SendAsync("RecieveStats", userStat);
        }
    }
}