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
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest : TestBase
    {
        private readonly IJourneyService journeyService;
        private readonly IRequestService requestService;
        private readonly Mock<IRepository<Request>> requestRepository;
        private readonly Mock<IRepository<Journey>> journeyRepository;

        public JourneyServiceTest()
        {
            journeyRepository = new Mock<IRepository<Journey>>();
            requestRepository = new Mock<IRepository<Request>>();
            journeyService = new JourneyService(
                journeyRepository.Object,
                requestRepository.Object,
                requestService,
                Mapper);
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
        [AutoEntityData]
        public async Task GetPastJourneysAsync_PastJourneysNotExist_ReturnsEmptyCollection([Range(1, 3)] int days, User participant)
        {
            // Arrange
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
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForOrganizer_ReturnsJourneyCollection([Range(1, 3)] int days, User organizer)
        {
            // Arrange
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
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysExistForParticipant_ReturnsJourneyCollection([Range(1, 3)] int days, User participant)
        {
            // Arrange
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
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UpcomingJourneysNotExistForOrganizer_ReturnsEmptyCollection([Range(1, 3)] int days, User organizer)
        {
            // Arrange
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
        [AutoEntityData]
        public async Task GetUpcomingJourneysAsync_UserNotHaveUpcomingJourneys_ReturnsEmptyCollection(
            [Range(1, 3)] int days, User organizer, User anotherUser)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForParticipant_ReturnsJourneyCollection(User participant)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysExistForOrganizer_ReturnsJourneyCollection(User organizer)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneysAsync_ScheduledJourneysNotExist_ReturnsEmptyCollection(User organizer)
        {
            // Arrange
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
        [AutoEntityData]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysExist_ReturnsStopCollection(
            [Range(1, 10)] int journeyCount, [Range(1, 5)] int countToTake, User organizer)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetStopsFromRecentJourneysAsync_RecentJourneysNotExist_ReturnsEmptyCollection(User organizer, List<Journey> recentJourneys)
        {
            // Arrange
            journeyRepository.Setup(r => r.Query())
                .Returns(recentJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetStopsFromRecentJourneysAsync(organizer.Id);

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
        public async Task GetFilteredJourneys_ReturnsJourneyCollection(JourneyFilter filter)
        {
            // Arrange
            var expectedJourneys = new List<Journey>();

            journeyRepository.Setup(r => r.Query())
                .Returns(expectedJourneys.AsQueryable().BuildMock().Object);

            // Act
            var result = await journeyService.GetFilteredJourneys(filter);

            // Assert
            result.Should().BeEquivalentTo(expectedJourneys);
        }

        [Theory]
        [InlineData(true, 3, 3)]
        [InlineData(false, 3, 0)]
        public async Task GetFilteredJourneys_FilteringFreeJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(journeysToCreateCount);

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
        [InlineData(true, 3, 0)]
        [InlineData(false, 3, 3)]
        public async Task GetFilteredJourneys_FilteringPaidJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(journeysToCreateCount);

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
        [InlineData(true, 3, 3)]
        [InlineData(false, 3, 3)]
        public async Task GetFilteredJourneys_FilteringAllFeeJourneys_ReturnsJourneysCollection(bool isFree, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.IsFree, isFree)
                .CreateMany(journeysToCreateCount);

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
        [InlineData("2121-1-1T12:00:00", "2121-1-1T12:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T14:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T10:00:00", 1, 1)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T15:00:00", 1, 0)]
        [InlineData("2121-1-1T12:00:00", "2121-1-1T09:00:00", 1, 0)]
        public async Task GetFilteredJourneys_FilteringByDepartureTime_ReturnsJourneysCollection(string journeyTime, string filterTime, int journeysToCreateCount, int expectedCount)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var journeys = journeyComposer
                .With(j => j.DepartureTime, DateTime.Parse(journeyTime))
                .CreateMany(journeysToCreateCount);

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
        [InlineData(3, 4, 1, 1, 1)]
        [InlineData(4, 4, 1, 1, 0)]
        [InlineData(2, 4, 4, 1, 0)]
        public async Task GetFilteredJourneys_FilteringIsEnoughSeats_ReturnsJourneysCollection(int participantsCountJourney, int countOfSeats, int passengersCountFilter, int journeysToCreateCount, int expectedCountOfJourneys)
        {
            // Arrange
            (var journeyComposer, var filterComposer) = GetInitializedJourneyAndFilter();

            var participants = Fixture.Build<User>().CreateMany(participantsCountJourney).ToList();

            var journeys = journeyComposer
                .With(j => j.Participants, participants)
                .With(j => j.CountOfSeats, countOfSeats)
                .CreateMany(journeysToCreateCount);

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

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyIsNotExist_ThrowDbUpdateConcurrencyException(int journeyIdToDelete)
        {
            // Arrange
            journeyRepository.Setup(repo =>
                repo.SaveChangesAsync()).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = journeyService.Invoking(service => service.DeleteAsync(journeyIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyExist_ExecuteOnce(int journeyIdToDelete)
        {
            // Act
            await journeyService.DeleteAsync(journeyIdToDelete);

            // Assert
            journeyRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetailsAsync_WhenJourneyIsValid_ReturnsJourneyObject(JourneyDto updatedJourneyDto)
        {
            // Arrange
            var journey = Mapper.Map<JourneyDto, Journey>(updatedJourneyDto);
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journey);

            journeyRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Journey>())).ReturnsAsync(journey);

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
            journeyRepository.Setup(repo =>
                    repo.UpdateAsync(It.IsAny<Journey>())).ReturnsAsync((Journey)null);

            // Act
            var result = await journeyService.UpdateDetailsAsync(updatedJourneyDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateRouteAsync_WhenJourneyIsValid_ReturnsJourneyObject(Journey[] journeys)
        {
            // Arrange
            var updatedJourneyDto = Fixture.Build<JourneyDto>()
                .With(dto => dto.Id, journeys.First().Id).Create();
            var expectedJourney = Mapper.Map<Journey, JourneyModel>(journeys.First());
            expectedJourney.Duration = updatedJourneyDto.Duration;
            expectedJourney.Stops = updatedJourneyDto.Stops;
            expectedJourney.JourneyPoints = updatedJourneyDto.JourneyPoints;
            expectedJourney.RouteDistance = updatedJourneyDto.RouteDistance;

            journeyRepository.Setup(r => r.Query())
                .Returns(journeys.AsQueryable().BuildMock().Object);

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
                .With(j => j.JourneyPoints, journeyPoints);

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
