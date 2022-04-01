using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Hubs;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class BadgeServiceTest : TestBase
    {
        private readonly IBadgeService badgeService;
        private readonly Mock<IRepository<UserStatistic>> userStatisticRepository;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IRepository<Journey>> journeyRepository;
        private readonly Mock<IJourneyService> journeyService;
        private readonly Mock<IHubContext<SignalRHub>> hub;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;

        public BadgeServiceTest()
        {
            userStatisticRepository = new Mock<IRepository<UserStatistic>>();
            userRepository = new Mock<IRepository<User>>();
            journeyRepository = new Mock<IRepository<Journey>>();
            journeyService = new Mock<IJourneyService>();
            hub = new Mock<IHubContext<SignalRHub>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            badgeService = new BadgeService(
                userStatisticRepository.Object,
                userRepository.Object,
                journeyRepository.Object,
                journeyService.Object,
                hub.Object,
                httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateJourneyStatistic_UncheckedJourneyExists_StatisticUpdated(
            User organizer,
            User passanger)
        {
            // Arrange
            var passangerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, passanger.Id)
                .Create();
            var organizerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, organizer.Id)
                .Create();
            var stats = new[] { organizerStats, passangerStats };

            var journeyUnmarked = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, new DateTime(2020, 5, 5))
                .With(j => j.Duration, new TimeSpan(5, 5, 5))
                .With(j => j.OrganizerId, organizer.Id)
                .With(j => j.Organizer, organizer)
                .With(j => j.Participants, new[] { passanger })
                .With(j => j.IsMarkedAsFinished, false)
                .Create();
            journeyRepository.Setup(r => r.Query()).Returns(
                new[] { journeyUnmarked }.AsQueryable().BuildMock().Object);
            journeyService.Setup(j => j.GetUncheckedJourneysAsync())
                .ReturnsAsync(new[] { journeyUnmarked }.AsEnumerable());

            userStatisticRepository.Setup(r => r.Query())
                .Returns(stats.AsQueryable().BuildMock().Object);

            userStatisticRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(It.IsAny<int>);

            foreach (var stat in stats)
            {
                hub.Setup(hub => hub.Clients.Group($"statistic{stat.Id}")).Returns(Mock.Of<IClientProxy>());
            }

            // Act
            await badgeService.UpdateStatisticsAsync();

            // Assert
            userStatisticRepository.Verify(
                repository => repository.SaveChangesAsync(),
                Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateJourneyStatistic_NoUncheckedJourneyExists_NeverExecuted(
            User organizer,
            User passanger)
        {
            // Arrange
            var passangerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, passanger.Id)
                .Create();
            var organizerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, organizer.Id)
                .Create();
            var stats = new[] { organizerStats, passangerStats };

            var journeyUnmarked = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, new DateTime(2020, 5, 5))
                .With(j => j.Duration, new TimeSpan(5, 5, 5))
                .With(j => j.OrganizerId, organizer.Id)
                .With(j => j.Organizer, organizer)
                .With(j => j.Participants, new[] { passanger })
                .With(j => j.IsMarkedAsFinished, true)
                .Create();
            journeyRepository.Setup(r => r.Query()).Returns(
                new[] { journeyUnmarked }.AsQueryable().BuildMock().Object);
            journeyService.Setup(j => j.GetUncheckedJourneysAsync())
                .ReturnsAsync(Array.Empty<Journey>().AsEnumerable());

            userStatisticRepository.Setup(r => r.Query())
                .Returns(stats.AsQueryable().BuildMock().Object);

            foreach (var stat in stats)
            {
                hub.Setup(hub => hub.Clients.Group($"statistic{stat.Id}")).Returns(Mock.Of<IClientProxy>());
            }

            // Act
            await badgeService.UpdateStatisticsAsync();

            // Assert
            userStatisticRepository.Verify(
                repository => repository.SaveChangesAsync(),
                Times.Never);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserStatistics_Participants_PassangerStatisticUpdated(
            [Range(100, 500)] int distance)
        {
            // Arrange
            var participant = Fixture.Build<User>()
                .Create();
            var participantStat = Fixture.Build<UserStatistic>()
                .With(p => p.Id, participant.Id)
                .Create();
            var users = new[] { participant }.ToList();

            var journeyNotMarked = Fixture.Build<Journey>()
                .With(j => j.RouteDistance, distance)
                .With(j => j.Participants, users)
                .With(j => j.IsMarkedAsFinished, false)
                .Create();
            var journeyMarked = Fixture.Build<Journey>()
                .With(j => j.IsMarkedAsFinished, true)
                .Create();
            journeyRepository.Setup(r => r.Query()).Returns(
                new[]
                {
                    journeyMarked,
                    journeyNotMarked,
                }.AsQueryable().BuildMock().Object);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(new[] { participantStat }.AsQueryable().BuildMock().Object);

            // Act
            await badgeService.UpdateStatisticsAsync();
            participantStat.PassangerJourneysAmount += 1;

            var result = badgeService.GetUserStatisticByUserIdAsync(participant.Id).Result;

            // Assert
            result.Should().BeEquivalentTo(participantStat);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserStatistics_JourneyUnmarked_SetsJourneyMarked(
            [Range(100, 500)] int distance)
        {
            // Arrange
            var participant = Fixture.Build<User>()
                .Create();
            var participantStat = Fixture.Build<UserStatistic>()
                .With(p => p.Id, participant.Id)
                .Create();
            var users = new[] { participant }.ToList();

            var journeyNotMarked = Fixture.Build<Journey>()
                .With(j => j.RouteDistance, distance)
                .With(j => j.Participants, users)
                .With(j => j.IsMarkedAsFinished, false)
                .Create();
            var journeyMarked = Fixture.Build<Journey>()
                .With(j => j.IsMarkedAsFinished, true)
                .Create();
            journeyRepository.Setup(r => r.Query()).Returns(
                new[]
                {
                    journeyMarked,
                    journeyNotMarked,
                }.AsQueryable().BuildMock().Object);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(new[] { participantStat }.AsQueryable().BuildMock().Object);

            // Act
            await badgeService.UpdateStatisticsAsync();

            var result = await journeyService.Object.GetUncheckedJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserStatistic_UserExists_ReturnsUserStatistic(User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query())
                .Returns(new[] { user }.AsQueryable());
            var users = new List<UserStatistic>();

            var userStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, user.Id)
                .Create();

            users.Add(userStats);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await badgeService.GetUserStatistic();

            // Assert
            result.Should().BeEquivalentTo(userStats);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserStatistic_NoUserStatisticsExist_ReturnsNull([Range(10, 15)] int userId)
        {
            // Arrange
            var user = Fixture.Build<User>()
                .With(u => u.Id, userId)
                .Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query())
                .Returns(new[] { user }.AsQueryable().BuildMock().Object);
            var users = new List<UserStatistic>();

            var otherUserStat = Fixture.Build<UserStatistic>()
                .With(u => u.Id, 30)
                .Create();
            users.Add(otherUserStat);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await badgeService.GetUserStatistic();

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserStatistic_NoUserExist_ReturnsNull([Range(-15, -10)] int userId)
        {
            // Arrange
            var user = Fixture.Build<User>()
                .With(u => u.Id, userId)
                .Create();
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query())
                .Returns(new[] { user }.AsQueryable().BuildMock().Object);
            var users = new List<UserStatistic>();

            var otherUserStat = Fixture.Build<UserStatistic>()
                .With(u => u.Id, 30)
                .Create();
            users.Add(otherUserStat);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await badgeService.GetUserStatistic();

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserStatisticByUserIdAsync_UserExists_ReturnsUserStatistic([Range(10, 15)] int userId)
        {
            // Arrange
            var users = new List<UserStatistic>();

            var userStat = Fixture.Build<UserStatistic>()
                .With(u => u.Id, userId)
                .Create();
            var otherUserStats = Fixture.Build<UserStatistic>()
                .CreateMany();
            users.AddRange(otherUserStats);
            users.Add(userStat);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await badgeService.GetUserStatisticByUserIdAsync(userId);

            // Assert
            result.Should().BeEquivalentTo(userStat);
        }
    }
}
