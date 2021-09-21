using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Filters;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest : TestBase
    {
        private readonly IJourneyService journeyService;
        private readonly Mock<IRequestService> requestService;
        private readonly Mock<IJourneyUserService> journeyUserService;
        private readonly Mock<INotificationService> notificationService;
        private readonly Mock<IRepository<Request>> requestRepository;
        private readonly Mock<IRepository<Journey>> journeyRepository;
        private readonly Mock<IRepository<ReceivedMessages>> receivedMessagesRepository;
        private readonly Mock<IRepository<Message>> messageRepository;
        private readonly Mock<IRepository<Chat>> chatRepository;
        private readonly Mock<IRepository<Schedule>> scheduleRepository;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IRepository<Invitation>> invitationRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<ILocationService> locationService;

        public JourneyServiceTest()
        {
            journeyRepository = new Mock<IRepository<Journey>>();
            requestRepository = new Mock<IRepository<Request>>();
            scheduleRepository = new Mock<IRepository<Schedule>>();
            requestService = new Mock<IRequestService>();
            locationService = new Mock<ILocationService>();
            journeyUserService = new Mock<IJourneyUserService>();
            userRepository = new Mock<IRepository<User>>();
            invitationRepository = new Mock<IRepository<Invitation>>();
            chatRepository = new Mock<IRepository<Chat>>();
            messageRepository = new Mock<IRepository<Message>>();
            receivedMessagesRepository = new Mock<IRepository<ReceivedMessages>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();

            notificationService = new Mock<INotificationService>();
            journeyService = new JourneyService(
                journeyRepository.Object,
                requestRepository.Object,
                userRepository.Object,
                scheduleRepository.Object,
                invitationRepository.Object,
                chatRepository.Object,
                notificationService.Object,
                requestService.Object,
                locationService.Object,
                journeyUserService.Object,
                Mapper,
                httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyByIdAsync_JourneyExists_ReturnsJourneyObject(List<Journey> journeys)
        {
            // Arrange
            var journey = Fixture.Build<Journey>()
                .With(j => j.Id, journeys.Max(j => j.Id) + 1)
                .Create();
            journeys.Add(journey);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            var expected = Mapper.Map<Journey, JourneyModel>(journey);

            // Act
            var result = await journeyService.GetJourneyByIdAsync(journey.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyByIdAsync_JourneyNotExist_ReturnsNull(Journey[] journeys)
        {
            // Arrange
            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetJourneyByIdAsync(journeys.Max(j => j.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetPastJourneysAsync_PastJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days, User participant)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, participant.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany()
                .ToList();
            var pastJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany().ToList();
            journeys.AddRange(pastJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(pastJourneys);

            // Act
            var result = await journeyService.GetPastJourneysAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetPastJourneysAsync_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days, User participant)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, participant.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetPastJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_UserNotHaveJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .CreateMany()
                .ToList();

            var user = Fixture.Build<User>()
                .With(
                    u => u.Id,
                    journeys.SelectMany(j => j.Participants.Select(p => p.Id)).Union(journeys.Select(j => j.OrganizerId)).Max() + 1)
                .Create();

            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetPastJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForOrganizer_ReturnsJourneyCollection([Range(1, 3)] int days, User organizer)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .With(j => j.Stops, new List<Stop>() { new Stop() { IsCancelled = false } })
                .With(j => j.JourneyUsers, new List<JourneyUser>())
                .With(j => j.Schedule, null as Schedule)
                .CreateMany()
                .ToList();
            var upcomingJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.OrganizerId, organizer.Id)
                .With(j => j.IsCancelled, false)
                .With(j => j.JourneyUsers, new List<JourneyUser>())
                .With(j => j.Schedule, null as Schedule)
                .CreateMany()
                .ToList();
            journeys.AddRange(upcomingJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForParticipant_ReturnsJourneyCollection([Range(1, 3)] int days, User participant)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, participant.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany()
                .ToList();
            var upcomingJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany()
                .ToList();
            journeys.AddRange(upcomingJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysNotExistForOrganizer_ReturnsEmptyCollection([Range(1, 3)] int days, User organizer)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection(
            [Range(1, 3)] int days, User organizer, User anotherUser)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.OrganizerId, anotherUser.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForParticipant_ReturnsJourneyCollection(User participant)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, participant.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .CreateMany()
                .ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.Participants, new List<User>() { participant })
                .With(j => j.IsCancelled, false)
                .CreateMany()
                .ToList();
            scheduledJourneys.ForEach(journey => journey.Schedule.Id = journey.Id);
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query())
              .Returns(journeys.AsQueryable().BuildMock().Object);

            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForOrganizer_ReturnsJourneyCollection(User organizer)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .With(j => j.IsCancelled, false)
                .With(j => j.JourneyUsers, new List<JourneyUser>())
                .CreateMany()
                .ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id)
                .With(j => j.JourneyUsers, new List<JourneyUser>())
                .With(j => j.IsCancelled, false)
                .CreateMany()
                .ToList();
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query())
             .Returns(journeys.AsQueryable().BuildMock().Object);

            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync();

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysNotExist_ReturnsEmptyCollection(User organizer)
        {
            // Arrange
            var claims = new List<Claim>() { new(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany().ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id + 1)
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query())
             .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysExist_ReturnsStopCollection(
            [Range(1, 10)] int journeyCount, [Range(1, 5)] int countToTake, User organizer)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var recentJourneys = Fixture.Build<Journey>()
                .With(journey => journey.OrganizerId, organizer.Id + 1)
                .CreateMany(journeyCount);

            journeyRepository.Setup(r => r.Query())
                .Returns(recentJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetStopsFromRecentJourneysAsync(countToTake);

            // Assert
            result.Should().HaveCountLessOrEqualTo(countToTake);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysNotExist_ReturnsEmptyCollection(User organizer, List<Journey> recentJourneys)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, organizer.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            journeyRepository.Setup(r => r.Query())
                .Returns(recentJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetStopsFromRecentJourneysAsync();

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeletePastJourneyAsync_DeletesRecordsInDb(List<Journey> journeysToDelete)
        {
            // Arrange
            journeyRepository.Setup(r => r.Query())
                .Returns(journeysToDelete.AsQueryable().BuildMock().Object);
            journeyRepository.Setup(r => r.DeleteRangeAsync(It.IsAny<List<Journey>>()))
                .Returns(Task.CompletedTask);

            // Act
            await journeyService.DeletePastJourneyAsync();

            // Assert
            journeyRepository.Verify(mock => mock.DeleteRangeAsync(It.IsAny<List<Journey>>()), Times.Once);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddAsync_WhenJourneyIsValid_ReturnsJourneyObject(JourneyDto journeyDto)
        {
            // Arrange
            journeyDto.WeekDay = null;
            var addedJourney = Mapper.Map<JourneyDto, Journey>(journeyDto);
            var journeyModel = Mapper.Map<Journey, JourneyModel>(addedJourney);

            journeyRepository.Setup(r =>
                r.AddAsync(It.IsAny<Journey>())).ReturnsAsync(addedJourney);

            // Act
            var result = await journeyService.AddJourneyAsync(journeyDto);

            // Assert
            result.Should().BeEquivalentTo(journeyModel, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task AddAsync_WhenJourneyIsNotValid_ReturnsJourneyObject(JourneyDto journeyDto)
        {
            // Arrange
            journeyRepository.Setup(r =>
                r.AddAsync(It.IsAny<Journey>())).ReturnsAsync((Journey)null);

            // Act
            var result = await journeyService.AddJourneyAsync(journeyDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public void GetFilteredJourneys_ReturnsJourneyCollection(JourneyFilter filter)
        {
            // Arrange
            var expectedJourneys = new List<Journey>();

            if (expectedJourneys == null)
            {
                throw new ArgumentNullException(nameof(expectedJourneys));
            }

            journeyRepository.Setup(r => r.Query())
                .Returns(expectedJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = journeyService.GetFilteredJourneys(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedJourneys);
        }

        [Theory]
        [InlineData(true, 3, 3)]
        [InlineData(false, 3, 0)]
        public void GetFilteredJourneys_FilteringFreeJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysToCreateCount);

            var freeFilter = filterComposer
                .With(f => f.Fee, FeeType.Free)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var freeResult = journeyService.GetFilteredJourneys(freeFilter);

            // Assert
            freeResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(true, 3, 0)]
        [InlineData(false, 3, 3)]
        public void GetFilteredJourneys_FilteringPaidJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysToCreateCount);

            var paidFilter = filterComposer
                .With(f => f.Fee, FeeType.Paid)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var paidResult = journeyService.GetFilteredJourneys(paidFilter);

            // Assert
            paidResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(true, 3, 3)]
        [InlineData(false, 3, 3)]
        public void GetFilteredJourneys_FilteringAllFeeJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysToCreateCount);

            var allFilter = filterComposer
                .With(f => f.Fee, FeeType.All)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var allResult = journeyService.GetFilteredJourneys(allFilter);

            // Assert
            allResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T12:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T14:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T10:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T15:00:00", 1, 0)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T09:00:00", 1, 0)]
        public void GetFilteredJourneys_FilteringByDepartureTime_ReturnsJourneysCollection(string journeyTime, string filterTime, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.DepartureTime, DateTime.Parse(journeyTime))
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysToCreateCount);

            var filter = filterComposer
                .With(f => f.DepartureTime, DateTime.Parse(filterTime))
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = journeyService.GetFilteredJourneys(filter);

            // Assert
            result.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(3, 4, 1, 1, 1)]
        [InlineData(4, 4, 1, 1, 0)]
        [InlineData(2, 4, 4, 1, 0)]
        public void GetFilteredJourneys_FilteringIsEnoughSeats_ReturnsJourneysCollection(int participantsCountJourney, int countOfSeats, int passengersCountFilter, int journeysToCreateCount, int expectedCountOfJourneys)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();

            var participants = Fixture.Build<User>().CreateMany(participantsCountJourney).ToList();

            var journeys = journeyComposer
                .With(j => j.Participants, participants)
                .With(j => j.CountOfSeats, countOfSeats)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysToCreateCount);

            var allFilter = filterComposer
                .With(f => f.PassengersCount, passengersCountFilter)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var allResult = journeyService.GetFilteredJourneys(allFilter);

            // Assert
            allResult.Should().HaveCount(expectedCountOfJourneys);
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyIsNotExist_ExecuteNever(int journeyIdToDelete)
        {
            // Arrange
            var journeys = new List<Journey>();

            if (journeys == null)
            {
                throw new ArgumentNullException(nameof(journeys));
            }

            journeyRepository.Setup(repo =>
                repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.DeleteAsync(journeyIdToDelete);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyExistAndUserIsOrganizer_ExecuteOnce(JourneyDto journeyDto)
        {
            // Arrange
            Journey journey = Mapper.Map<JourneyDto, Journey>(journeyDto);
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, journey.OrganizerId.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Participants, null as List<User>)
                .With(j => j.Id, journey.Id)
                .With(j => j.OrganizerId, journey.OrganizerId)
                .CreateMany(1);

            notificationService.Setup(s => s.NotifyParticipantsAboutCancellationAsync(It.IsAny<Journey>())).Returns(Task.CompletedTask);

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.DeleteAsync(journey.Id);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyExistAndUserIsNotOrganizer_ExecuteNever(JourneyDto journeyDto)
        {
            // Arrange
            Journey journey = Mapper.Map<JourneyDto, Journey>(journeyDto);
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, (journey.OrganizerId + 1).ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Participants, null as List<User>)
                .With(j => j.Id, journey.Id)
                .With(j => j.OrganizerId, journey.OrganizerId)
                .CreateMany(1);

            notificationService.Setup(s => s.NotifyParticipantsAboutCancellationAsync(It.IsAny<Journey>())).Returns(Task.CompletedTask);

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.DeleteAsync(journey.Id);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoData]
        public async Task CancelAsync_WhenJourneyDoesNotExist_ExecutesNever(int journeyIdToCancel)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Participants, null as List<User>)
                .With(j => j.Id, -journeyIdToCancel)
                .CreateMany(5);

            journeyRepository.Setup(repo =>
                repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.CancelAsync(journeyIdToCancel);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoData]
        public async Task CancelAsync_WhenJourneyDoesExist_ExecuteOnce(int journeyIdToCancel)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Participants, null as List<User>)
                .With(j => j.Id, journeyIdToCancel)
                .CreateMany(1);
            var schedules = Fixture.Build<Schedule>()
                .CreateMany(0);

            notificationService.Setup(s => s.NotifyParticipantsAboutCancellationAsync(It.IsAny<Journey>())).Returns(Task.CompletedTask);
            notificationService.Setup(s => s.DeleteNotificationsAsync(It.IsAny<IEnumerable<Notification>>()));
            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);
            scheduleRepository.Setup(repo =>
                repo.Query()).Returns(schedules.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.CancelAsync(journeyIdToCancel);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetailsAsync_WhenJourneyIsValid_ReturnsJourneyObject(JourneyDto updatedJourneyDto)
        {
            // Arrange
            updatedJourneyDto.WeekDay = null;
            var journey = Mapper.Map<JourneyDto, Journey>(updatedJourneyDto);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journey.Id)
                .With(dto => dto.IsCancelled, false)
                .With(dto => dto.DepartureTime, DateTime.Now.AddHours(1))
                .CreateMany(1);
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journey);
            var schedules = Fixture.Build<Schedule>()
                .CreateMany(0);

            journeyRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Journey>())).ReturnsAsync(journey);
            journeyRepository.Setup(repo =>
                    repo.Query()).Returns(journeys.AsQueryable().BuildMock().Object);
            scheduleRepository.Setup(repo =>
                repo.Query()).Returns(schedules.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateDetailsAsync(updatedJourneyDto);

            // Assert
            result.Should().BeEquivalentTo(expectedJourney);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetailsAsync_WhenJourneyIsNotValid_ReturnsNull(JourneyDto updatedJourneyDto)
        {
            // Arrange
            updatedJourneyDto.WeekDay = null;
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, updatedJourneyDto.Id + 1)
                .CreateMany(1);
            var schedules = Fixture.Build<Schedule>()
                .CreateMany(0);

            journeyRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Journey>())).ReturnsAsync((Journey)null);
            journeyRepository.Setup(repo =>
                    repo.Query()).Returns(journeys.AsQueryable().BuildMock().Object);
            scheduleRepository.Setup(repo =>
                repo.Query()).Returns(schedules.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateDetailsAsync(updatedJourneyDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateInvitationAsync_WhenInvitationIsValid_ReturnsInvitationObject(
            InvitationDto updatedInvitationDto,
            JourneyDto updatedJourneyDto)
        {
            // Arrange
            updatedInvitationDto.JourneyId = updatedJourneyDto.Id;
            var journey = Mapper.Map<JourneyDto, Journey>(updatedJourneyDto);
            var invitation = Mapper.Map<InvitationDto, Invitation>(updatedInvitationDto);
            var invitations = Fixture.Build<Invitation>()
                .With(i => i.Id, invitation.Id)
                .With(i => i.InvitedUserId, invitation.InvitedUserId)
                .CreateMany(1);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journey.Id)
                .With(j => j.Invitations, invitations.ToList())
                .CreateMany(1);
            var expectedInvitation = Mapper.Map<Invitation, InvitationDto>(invitation);

            invitationRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Invitation>())).ReturnsAsync(invitation);
            invitationRepository.Setup(repo =>
                    repo.Query()).Returns(invitations.AsQueryable().BuildMock().Object);
            journeyRepository.Setup(repo =>
                    repo.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateInvitationAsync(updatedInvitationDto);

            // Assert
            result.Should().BeEquivalentTo(expectedInvitation);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateInvitationAsync_WhenInvitationIsNotValid_ReturnsInvitationObject(
            InvitationDto updatedInvitationDto,
            JourneyDto updatedJourneyDto)
        {
            // Arrange
            updatedInvitationDto.JourneyId = updatedJourneyDto.Id;
            var journey = Mapper.Map<JourneyDto, Journey>(updatedJourneyDto);
            var invitation = Mapper.Map<InvitationDto, Invitation>(updatedInvitationDto);
            var invitations = Fixture.Build<Invitation>()
                .With(i => i.Id, invitation.Id)
                .With(i => i.InvitedUserId, invitation.InvitedUserId)
                .CreateMany(1);
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journey.Id)
                .With(j => j.Invitations, invitations.ToList())
                .CreateMany(1);
            var expectedInvitation = Mapper.Map<Invitation, InvitationDto>(invitation);

            invitationRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Invitation>())).ReturnsAsync((Invitation)null);
            invitationRepository.Setup(repo =>
                    repo.Query()).Returns(invitations.AsQueryable().BuildMock().Object);
            journeyRepository.Setup(repo =>
                    repo.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateInvitationAsync(updatedInvitationDto);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateRouteAsync_WhenJourneyIsValid_ReturnsJourneyObject()
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.Now + TimeSpan.FromDays(1))
                .CreateMany();
            var updatedJourneyDto = Fixture.Build<JourneyDto>()
                .With(dto => dto.Id, journeys.First().Id)
                .With(dto => dto.WeekDay, () => null)
                .Create();
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journeys.First());
            expectedJourney.Duration = updatedJourneyDto.Duration;
            expectedJourney.Stops = updatedJourneyDto.Stops;
            expectedJourney.JourneyPoints = updatedJourneyDto.JourneyPoints;
            expectedJourney.RouteDistance = updatedJourneyDto.RouteDistance;
            var schedules = Fixture.Build<Schedule>()
                .CreateMany(0);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            scheduleRepository.Setup(repo =>
                repo.Query()).Returns(schedules.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateRouteAsync(updatedJourneyDto);

            // Assert
            result.Should().BeEquivalentTo(expectedJourney);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateRouteAsync_WhenJourneyIsNotValid_ReturnsNull(Journey[] journeys)
        {
            // Arrange
            var updatedJourneyDto = Fixture.Build<JourneyDto>()
                .With(dto => dto.Id, journeys.Max(journey => journey.Id) + 1)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.UpdateRouteAsync(updatedJourneyDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [InlineAutoData(3)]
        public void GetApplicantJourneys_SuitableJourneysExist_ReturnsNotEmptyApplicantJourneyCollection(int journeysCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();
            var journeys = journeyComposer
                .With(j => j.IsFree, false)
                .With(j => j.Schedule, null as Schedule)
                .CreateMany(journeysCount)
                .ToList();

            var filter = filterComposer
                .With(f => f.Fee, FeeType.All)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = journeyService.GetApplicantJourneys(filter).ToList();

            // Assert
            result.Should().HaveCount(journeys.Count);
            result.Should().BeOfType<List<ApplicantJourney>>();
        }

        [Theory]
        [InlineAutoData(3)]
        public void GetApplicantJourneys_SuitableJourneysNotExist_ReturnsEmptyApplicantJourneyCollection(int journeysCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();
            var journeys = journeyComposer
                .With(j => j.IsFree, false)
                .CreateMany(journeysCount);

            var filter = filterComposer
                .With(f => f.Fee, FeeType.Free)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = journeyService.GetApplicantJourneys(filter).ToList();

            // Assert
            result.Should().BeEmpty();
            result.Should().BeOfType<List<ApplicantJourney>>();
        }

        [Theory]
        [AutoData]
        public async Task CheckForSuitableRequests_SuitableRequestsFound_DoesNotifyUser(int filtersCount)
        {
            // Arrange
            var (journeyComposer, filterComposer) = GetInitializedJourneyAndFilter();
            var journey = journeyComposer
                .With(j => j.IsFree, false)
                .Create();

            var filters = filterComposer
                .With(f => f.Fee, FeeType.All)
                .CreateMany(filtersCount);

            var requests = Mapper.Map<IEnumerable<JourneyFilter>, IEnumerable<Request>>(filters);

            requestRepository.Setup(r => r.Query())
                .Returns(requests.AsQueryable().BuildMock().Object);

            requestService
                .Setup(r =>
                    r.NotifyUserAsync(
                        It.IsAny<RequestDto>(),
                        It.IsAny<Journey>(),
                        It.IsAny<IEnumerable<StopDto>>()))
                .Returns(Task.CompletedTask);

            // Act
            await journeyService.CheckForSuitableRequests(journey);

            // Assert
            requestService.Verify(
                r => r.NotifyUserAsync(
                It.IsAny<RequestDto>(),
                It.IsAny<Journey>(),
                It.IsAny<IEnumerable<StopDto>>()),
                Times.AtLeastOnce);
        }

        [Theory]
        [AutoData]
        public async Task IsCanceled_WhenJourneyExists_ReturnsIsCanceledJourneyPropertyValue(int journeyId, bool isCancelled)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId)
                .With(j => j.IsCancelled, isCancelled)
                .CreateMany(1);

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.IsCanceled(journeyId);

            // Assert
            result.Should().Be(isCancelled);
        }

        [Theory]
        [AutoData]
        public async Task IsCanceled_WhenJourneyDoesntExist_ReturnsFalse(int journeyId, bool isCancelled)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, -journeyId)
                .With(j => j.IsCancelled, isCancelled)
                .CreateMany(1);

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.IsCanceled(journeyId);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserFromJourney_WhenUserExists_ExecuteOnce(int journeyId, int userId)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, userId.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            var user = Fixture.Build<User>()
                .With(u => u.Id, userId)
                .Create();

            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId)
                .With(j => j.Participants, new List<User> { user })
                .CreateMany(1)
                .ToList();

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            notificationService.Setup(n => n.NotifyDriverAboutParticipantWithdrawal(It.IsAny<Journey>(), It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await journeyService.DeleteUserFromJourney(journeyId, userId);

            // Assert
            journeys.First().Participants.Any(u => u.Id == userId).Should().BeFalse();
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserFromJourney_WhenUserDoesNotExist_ExecuteNever(int journeyId, int userId)
        {
            // Arrange
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId)
                .With(j => j.Participants, new List<User>())
                .CreateMany(1);

            journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            await journeyService.DeleteUserFromJourney(journeyId, userId);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserToJourney_WhenJourneyDoesNotExist_ReturnsFalse(JourneyApplyModel journeyApply)
        {
            // Arrange
            if (journeyApply.JourneyUser != null)
            {
               var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyApply.JourneyUser.JourneyId + 1)
                .With(j => j.Participants, new List<User>())
                .CreateMany(1);
               var participants = Fixture.Build<User>()
                .With(p => p.Id, journeyApply.JourneyUser.UserId)
                .CreateMany(1)
                .ToList();
               journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);
               userRepository.Setup(r => r.Query()).Returns(participants.AsQueryable().BuildMock().Object);
            }

            // Act
            var result = await journeyService.AddUserToJourney(journeyApply);

            // Assert
            result.Should().Be(false);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserToJourney_WhenJourneyAndUserAreValid_ReturnsTrue(JourneyApplyModel journeyApply)
        {
            // Arrange
            if (journeyApply.JourneyUser != null)
            {
                var receivedMessages = Fixture.Build<ReceivedMessages>()
                .With(rm => rm.ChatId, journeyApply.JourneyUser.JourneyId)
                .CreateMany(1)
                .ToList();
                var participants = Fixture.Build<User>()
                .With(p => p.Id, journeyApply.JourneyUser.UserId)
                .With(p => p.ReceivedMessages, receivedMessages)
                .CreateMany(1)
                .ToList();
                var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyApply.JourneyUser.JourneyId)
                .With(j => j.Participants, new List<User>())
                .With(j => j.CountOfSeats, 4).
                CreateMany(1)
                .ToList();
                var chats = Fixture.Build<Chat>()
                .With(c => c.Id, journeyApply.JourneyUser.JourneyId)
                .With(c => c.ReceivedMessages, new List<ReceivedMessages>())
                .CreateMany(1)
                .ToList();

                journeyRepository.Setup(r => r.Query()).Returns(journeys.AsQueryable().BuildMock().Object);
                userRepository.Setup(r => r.Query()).Returns(participants.AsQueryable().BuildMock().Object);
                chatRepository.Setup(r => r.Query()).Returns(chats.AsQueryable().BuildMock().Object);
                receivedMessagesRepository.Setup(r => r.Query()).Returns(receivedMessages.AsQueryable().BuildMock().Object);
            }

            // Act
            var result = await journeyService.AddUserToJourney(journeyApply);

            // Assert
            result.Should().Be(true);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUnreadMessagesCountForNewUser_UserGainsUnreadMessagesEqualsToTotalMessages(int journeyId)
        {
            // Arrange
            var messages = Fixture.Build<Message>()
                .With(m => m.ChatId, journeyId)
                .CreateMany(5)
                .ToList();
            var chats = Fixture.Build<Chat>()
                .With(c => c.Id, journeyId)
                .With(c => c.Messages, messages)
                .CreateMany(1)
                .ToList();
            messageRepository.Setup(r => r.Query()).Returns(messages.AsQueryable().BuildMock().Object);
            chatRepository.Setup(r => r.Query()).Returns(chats.AsQueryable().BuildMock().Object);
            var expected = messages.Count;

            // Act
            var result = await journeyService.GetUnreadMessagesCountForNewUserAsync(journeyId);

            // Assert
            result.Should().Be(expected);
         }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyByIdAsync_JourneyAndJourneyUserExist_ReturnsTupleWithNotNullItems(
            int journeyId,
            int userId)
        {
            // Assert
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId)
                .With(j => j.IsCancelled, false)
                .CreateMany(1)
                .ToList();
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journeys.First());

            var journeyUser = Fixture.Build<JourneyUserDto>()
                .With(ju => ju.JourneyId, journeyId)
                .With(ju => ju.UserId, userId)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            journeyUserService.Setup(s => s.GetJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(journeyUser);

            // Act
            var result = await journeyService.GetJourneyWithJourneyUserByIdAsync(journeyId, userId, true);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Journey.Should().BeEquivalentTo(expectedJourney);
                result.JourneyUser.Should().BeEquivalentTo(journeyUser);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyByIdAsync_JourneyExistsButJourneyUserDoesNot_ReturnsTupleWithNotNullJourney(
            int journeyId,
            int userId)
        {
            // Assert
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId)
                .With(j => j.IsCancelled, false)
                .CreateMany(1)
                .ToList();
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journeys.First());

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            journeyUserService.Setup(s => s.GetJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((JourneyUserDto)null);

            // Act
            var result = await journeyService.GetJourneyWithJourneyUserByIdAsync(journeyId, userId, true);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Journey.Should().BeEquivalentTo(expectedJourney);
                result.JourneyUser.Should().BeNull();
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyByIdAsync_JourneyAndJourneyUserDoNotExist_ReturnsTupleWithNullItems(
            int journeyId,
            int userId)
        {
            // Assert
            var journeys = Fixture.Build<Journey>()
                .With(j => j.Id, journeyId + 1)
                .With(j => j.IsCancelled, false)
                .CreateMany(1);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            journeyUserService.Setup(s => s.GetJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((JourneyUserDto)null);

            // Act
            var result = await journeyService.GetJourneyWithJourneyUserByIdAsync(journeyId, userId, true);

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Journey.Should().BeNull();
                result.JourneyUser.Should().BeNull();
            }
        }

        private (IPostprocessComposer<Journey> Journeys, IPostprocessComposer<JourneyFilter> Filter) GetInitializedJourneyAndFilter()
        {
            var departureTime = DateTime.UtcNow.AddHours(1);

            var journeyPoints = new List<JourneyPoint>
                {
                    new JourneyPoint { Latitude = 30, Longitude = 30 },
                    new JourneyPoint { Latitude = 35, Longitude = 35 },
                };

            var journey = Fixture.Build<Journey>()
                .With(j => j.CountOfSeats, 4)
                .With(j => j.IsFree, true)
                .With(j => j.DepartureTime, departureTime)
                .With(j => j.JourneyPoints, journeyPoints)
                .With(j => j.IsCancelled, false);

            var filter = Fixture.Build<JourneyFilter>()
                .With(f => f.DepartureTime, departureTime)
                .With(f => f.PassengersCount, 1)
                .With(f => f.FromLatitude, 30)
                .With(f => f.FromLongitude, 30)
                .With(f => f.ToLatitude, 35)
                .With(f => f.ToLongitude, 35)
                .With(f => f.Fee, FeeType.All);

            return (journey, filter);
        }
    }
}
