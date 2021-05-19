using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Dsl;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Enums;
using Car.Data.Infrastructure;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest : TestBase
    {
        private readonly IJourneyService journeyService;
        private readonly Mock<IRepository<Journey>> journeyRepository;

        public JourneyServiceTest()
        {
            journeyRepository = new Mock<IRepository<Journey>>();
            journeyService = new JourneyService(journeyRepository.Object, Mapper);
        }

        [Fact]
        public async Task GetJourneyByIdAsync_JourneyExists_ReturnsJourneyObject()
        {
            // Arrange
            var journeys = Fixture.Create<List<Journey>>();
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

        [Fact]
        public async Task GetJourneyByIdAsync_JourneyNotExist_ReturnsNull()
        {
            // Arrange
            var journeys = Fixture.Create<Journey[]>();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetJourneyByIdAsync(journeys.Max(j => j.Id) + 1);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_PastJourneysExist_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var pastJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany().ToList();
            journeys.AddRange(pastJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(pastJourneys);

            // Act
            var result = await journeyService.GetPastJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoData]
        public async Task GetPastJourneysAsync_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetPastJourneysAsync(participant.Id);

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

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetPastJourneysAsync(user.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForOrganizer_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();
            var upcomingJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForParticipant_ReturnsJourneyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var participant = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany()
                .ToList();
            var upcomingJourneys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(days))
                .With(j => j.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(upcomingJourneys);

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);
            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(upcomingJourneys);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysNotExistForOrganizer_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(j => j.DepartureTime, DateTime.UtcNow.AddDays(-days))
                .With(j => j.OrganizerId, organizer.Id)
                .CreateMany()
                .ToList();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetUpcomingJourneysAsync_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection([Range(1, 3)] int days)
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var anotherUser = Fixture.Create<User>();
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
            var result = await journeyService.GetUpcomingJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForParticipant_ReturnsJourneyCollection()
        {
            // Arrange
            var participant = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.Participants, new List<User>() { participant })
                .CreateMany().ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.Participants, new List<User>() { participant })
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query(journey => journey.Schedule))
              .Returns(journeys.AsQueryable().BuildMock().Object);

            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(participant.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForOrganizer_ReturnsJourneyCollection()
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany().ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query(journey => journey.Schedule))
             .Returns(journeys.AsQueryable().BuildMock().Object);

            var expected = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(scheduledJourneys);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }

        [Fact]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var journeys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, (Schedule)null)
                .With(journey => journey.OrganizerId, organizer.Id)
                .CreateMany().ToList();
            var scheduledJourneys = Fixture.Build<Journey>()
                .With(journey => journey.Schedule, new Schedule())
                .With(journey => journey.OrganizerId, organizer.Id + 1)
                .CreateMany();
            journeys.AddRange(scheduledJourneys);

            journeyRepository.Setup(r => r.Query(journey => journey.Schedule))
             .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetScheduledJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [AutoData]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysExist_ReturnsStopCollection([Range(1, 10)] int journeyCount, [Range(1, 5)] int countToTake)
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var recentJourneys = Fixture.Build<Journey>()
                .With(journey => journey.OrganizerId, organizer.Id + 1)
                .CreateMany(journeyCount);

            journeyRepository.Setup(r => r.Query())
                .Returns(recentJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetStopsFromRecentJourneysAsync(organizer.Id, countToTake);

            // Assert
            result.Should().HaveCountLessOrEqualTo(countToTake);
        }

        [Fact]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var organizer = Fixture.Create<User>();
            var recentJourneys = new List<Journey>();

            journeyRepository.Setup(r => r.Query())
                .Returns(recentJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetStopsFromRecentJourneysAsync(organizer.Id);

            // Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task DeletePastJourneyAsync_DeletesRecordsInDb()
        {
            // Arrange
            var journeysToDelete = new List<Journey>();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeysToDelete.AsQueryable().BuildMock().Object);
            journeyRepository.Setup(r => r.DeleteRangeAsync(It.IsAny<List<Journey>>()))
                .Returns(Task.CompletedTask);

            // Act
            await journeyService.DeletePastJourneyAsync();

            // Assert
            journeyRepository.Verify(mock => mock.DeleteRangeAsync(It.IsAny<List<Journey>>()), Times.Once);
        }

        [Fact]
        public async Task AddAsync_WhenJourneyIsValid_ReturnsJourneyObject()
        {
            // Arrange
            var createJourneyModel = Fixture.Create<CreateJourneyModel>();
            var addedJourney = Mapper.Map<CreateJourneyModel, Journey>(createJourneyModel);
            var journeyModel = Mapper.Map<Journey, JourneyModel>(addedJourney);

            journeyRepository.Setup(r =>
                r.AddAsync(It.IsAny<Journey>())).ReturnsAsync(addedJourney);

            // Act
            var result = await journeyService.AddJourneyAsync(createJourneyModel);

            // Assert
            result.Should().BeEquivalentTo(journeyModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task AddAsync_WhenJourneyIsNotValid_ReturnsJourneyObject()
        {
            // Arrange
            var createJourneyModel = Fixture.Create<CreateJourneyModel>();

            journeyRepository.Setup(r =>
                r.AddAsync(It.IsAny<Journey>())).ReturnsAsync((Journey)null);

            // Act
            var result = await journeyService.AddJourneyAsync(createJourneyModel);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetFilteredJourneys_ReturnsJourneyCollection()
        {
            // Arrange
            var filter = Fixture.Create<JourneyFilterModel>();
            var expectedJourneys = new List<Journey>();

            journeyRepository.Setup(r => r.Query())
                .Returns(expectedJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetFilteredJourneys(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedJourneys);
        }

        [Theory]
        [InlineData(true, 3)]
        [InlineData(false, 0)]
        public async Task GetFilteredJourneys_FilteringFreeJourneys_ReturnsJourneysCollection(bool isFree, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(3);

            var freeFilter = filterComposer
                .With(f => f.Fee, FeeType.Free)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var freeResult = await journeyService.GetFilteredJourneys(freeFilter);

            // Assert
            freeResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(true, 0)]
        [InlineData(false, 3)]
        public async Task GetFilteredJourneys_FilteringPaidJourneys_ReturnsJourneysCollection(bool isFree, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(3);

            var paidFilter = filterComposer
                .With(f => f.Fee, FeeType.Paid)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var paidResult = await journeyService.GetFilteredJourneys(paidFilter);

            // Assert
            paidResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(true, 3)]
        [InlineData(false, 3)]
        public async Task GetFilteredJourneys_FilteringAllFeeJourneys_ReturnsJourneysCollection(bool isFree, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(3);

            var allFilter = filterComposer
                .With(f => f.Fee, FeeType.All)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var allResult = await journeyService.GetFilteredJourneys(allFilter);

            // Assert
            allResult.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData("2021-1-1T12:00:00", "2021-1-1T12:00:00", 1)]
        [InlineData("2021-1-1T12:00:00", "2021-1-1T14:00:00", 1)]
        [InlineData("2021-1-1T12:00:00", "2021-1-1T10:00:00", 1)]
        [InlineData("2021-1-1T12:00:00", "2021-1-1T15:00:00", 0)]
        [InlineData("2021-1-1T12:00:00", "2021-1-1T09:00:00", 0)]
        public async Task GetFilteredJourneys_FilteringByDepartureTime_ReturnsJourneysCollection(string journeyTime, string filterTime, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.DepartureTime, DateTime.Parse(journeyTime))
                .CreateMany(1);

            var filter = filterComposer
                .With(f => f.DepartureTime, DateTime.Parse(filterTime))
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetFilteredJourneys(filter);

            // Assert
            result.Should().HaveCount(expectedCount);
        }

        [Theory]
        [InlineData(3, 4, 1, 1)]
        [InlineData(4, 4, 1, 0)]
        [InlineData(2, 4, 4, 0)]
        public async Task GetFilteredJourneys_FilteringIsEnoughSeats_ReturnsJourneysCollection(int participantsCountJourney, int countOfSeats, int passengersCountFilter, int expectedCountOfJourneys)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var participants = Fixture.Build<User>().CreateMany(participantsCountJourney).ToList();

            var journeys = journeyComposer
                .With(j => j.Participants, participants)
                .With(j => j.CountOfSeats, countOfSeats)
                .CreateMany(1);

            var allFilter = filterComposer
                .With(f => f.PassengersCount, passengersCountFilter)
                .Create();

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

            // Act
            var allResult = await journeyService.GetFilteredJourneys(allFilter);

            // Assert
            allResult.Should().HaveCount(expectedCountOfJourneys);
        }

        [Fact]
        public async Task DeleteAsync_WhenCarIsNotExist_ThrowDbUpdateConcurrencyException()
        {
            // Arrange
            var journeyIdToDelete = Fixture.Create<int>();
            journeyRepository.Setup(repo =>
                repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = journeyService.Invoking(service => service.DeleteAsync(journeyIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task DeleteAsync_WhenCarExist_ExecuteOnce()
        {
            // Arrange
            var idCarToDelete = Fixture.Create<int>();

            // Act
            await journeyService.DeleteAsync(idCarToDelete);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        private (IPostprocessComposer<Journey> Journeys, IPostprocessComposer<JourneyFilterModel> Filter) GetInitializedJourneyAndFilter()
        {
            var departureTime = DateTime.Now;

            var journeyPoints = new List<JourneyPoint>
                {
                    new JourneyPoint { Latitude = 30, Longitude = 30 },
                    new JourneyPoint { Latitude = 35, Longitude = 35 },
                };

            var journey = Fixture.Build<Journey>()
                .With(j => j.CountOfSeats, 4)
                .With(j => j.IsFree, true)
                .With(j => j.DepartureTime, departureTime)
                .With(j => j.JourneyPoints, journeyPoints);

            var filter = Fixture.Build<JourneyFilterModel>()
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
