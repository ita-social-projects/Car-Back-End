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
        public async Task UpdateUserStatistics_OnOwnCar_OrganizerDriverStatisticUpdated(
            [Range(100, 500)] int distance,
            User organizer,
            [Range(1, 4)] int participantCount)
        {
            // Arrange
            var participants = Fixture.Build<User>()
                .CreateMany(participantCount);
            var organizerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, organizer.Id)
                .Create();

            var journeyNotMarked = Fixture.Build<Journey>()
                .With(j => j.RouteDistance, distance)
                .With(j => j.Organizer, organizer)
                .With(j => j.Participants, participants.ToList())
                .With(j => j.IsMarkedAsFinished, false)
                .With(j => j.IsOnOwnCar, true)
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

            var users = participants.ToList();
            users.Add(organizer);
            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(new[] { organizerStats }.AsQueryable().BuildMock().Object);

            // Act
            await badgeService.UpdateStatisticsAsync();
            organizerStats.DriverJourneysAmount += 1;
            organizerStats.TotalKm += journeyNotMarked.RouteDistance;

            var result = badgeService.GetUserStatisticByUserIdAsync(organizer.Id).Result;

            // Assert
            result.Should().BeEquivalentTo(organizerStats);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserStatistics_NotOnOwnCar_OrganizerPassangerStatisticUpdated(
            [Range(100, 500)] int distance,
            User organizer,
            [Range(1, 4)] int participantCount)
        {
            // Arrange
            var participants = Fixture.Build<User>()
                .CreateMany(participantCount);
            var organizerStats = Fixture.Build<UserStatistic>()
                .With(u => u.Id, organizer.Id)
                .Create();

            var journeyNotMarked = Fixture.Build<Journey>()
                .With(j => j.RouteDistance, distance)
                .With(j => j.Organizer, organizer)
                .With(j => j.Participants, participants.ToList())
                .With(j => j.IsMarkedAsFinished, false)
                .With(j => j.IsOnOwnCar, false)
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

            var users = participants.ToList();
            users.Add(organizer);
            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            userStatisticRepository.Setup(r => r.Query())
                .Returns(new[] { organizerStats }.AsQueryable().BuildMock().Object);

            // Act
            await badgeService.UpdateStatisticsAsync();
            organizerStats.PassangerJourneysAmount += 1;

            var result = badgeService.GetUserStatisticByUserIdAsync(organizer.Id).Result;

            // Assert
            result.Should().BeEquivalentTo(organizerStats);
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
        public async Task GetUserStatistic_UserStatisticsNotExists_ReturnsNull([Range(10, 15)] int userId)
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
