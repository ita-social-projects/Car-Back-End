using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class JourneyServiceTest
    {
        private readonly IJourneyService journeyService;
        private readonly Mock<IRepository<Journey>> repository;
        private readonly Mock<IUnitOfWork<Journey>> unitOfWork;
        private readonly Fixture fixture;

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            unitOfWork = new Mock<IUnitOfWork<Journey>>();

            journeyService = new JourneyService(unitOfWork.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public Journey GetJourney() =>
            new Journey
            {
                Id = It.IsAny<int>(),
                DepartureTime = DateTime.Now,
                Organizer = fixture.Create<User>(),
                OrganizerId = 0,
                Duration = new TimeSpan(0, 15, 0),
                Participants = fixture.Create<List<User>>(),
                Schedule = fixture.Create<Schedule>(),
            };

        [Theory]
        [AutoData]
        public void TestGetCurrentJourneyForOrganizer_WhenCurrentJourneyExists_ReturnsJourneyObject([Range(1, 3)] int hours, [Range(1, 3)] double divider)
        {
            var journeys = fixture.Create<List<Journey>>();
            var currentJourney = fixture.Create<Journey>();
            currentJourney.Duration = new TimeSpan(0, hours, 0, 0);
            currentJourney.DepartureTime = DateTime.Now.Subtract(currentJourney.Duration.Divide(divider));
            journeys.Add(currentJourney);

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys.AsQueryable);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(currentJourney.OrganizerId);

            result.Should().BeEquivalentTo(currentJourney);
        }

        [Theory]
        [AutoData]
        public void TestGetCurrentJourneyForParticipant_WhenCurrentJourneyExists_ReturnsJourneyObject([Range(1, 3)] int hours, [Range(1, 3)] double divider)
        {
            var journeys = fixture.Create<List<Journey>>();
            var currentJourney = fixture.Create<Journey>();
            currentJourney.Duration = new TimeSpan(0, hours, 0, 0);
            currentJourney.DepartureTime = DateTime.Now.Subtract(currentJourney.Duration.Divide(divider));
            journeys.Add(currentJourney);

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys.AsQueryable);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(currentJourney.Participants?.FirstOrDefault()?.Id ?? 0);

            result.Should().BeEquivalentTo(currentJourney);
        }

        [Theory]
        [AutoData]
        public void TestGetCurrentJourney_WhenCurrentJourneyNotExist_ReturnsNull([Range(1, 3)] int hours)
        {
            var journeys = fixture.Create<List<Journey>>();
            var currentJourney = fixture.Create<Journey>();
            currentJourney.Duration = new TimeSpan(0, hours, 0, 0);
            currentJourney.DepartureTime = DateTime.Now.Add(currentJourney.Duration);
            journeys.Add(currentJourney);

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys.AsQueryable);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(currentJourney.Participants?.FirstOrDefault()?.Id ?? 0);

            result.Should().BeNull();
        }

        [Fact]
        public void TestGetPastJourneys_ReturnsJourneyObject()
        {
            var pastJourney = GetJourney();
            pastJourney.DepartureTime = DateTime.Now.AddMinutes(-30);
            var journeys = new List<Journey> { pastJourney }.AsQueryable();

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetPastJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journeys);
        }

        [Fact]
        public void TestGetUpcomingJourneys_ReturnsJourneyObject()
        {
            var upcomingJourney = GetJourney();
            upcomingJourney.DepartureTime = DateTime.Now.AddMinutes(30);
            var journeys = new List<Journey> { upcomingJourney }.AsQueryable();

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journeys);
        }

        [Fact]
        public void TestGetScheduledJourneys_ReturnsJourneyObject()
        {
            var scheduledJourney = GetJourney();
            var journeys = new List<Journey> { scheduledJourney }.AsQueryable();

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetScheduledJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journeys);
        }

        [Fact]
        public void TestGetJourneyById_WhenJourneyExists_ReturnsJourneyObject()
        {
            var journeys = fixture.Create<Journey[]>().AsQueryable();
            var journey = journeys.FirstOrDefault();

            repository.Setup(r => r.Query(j => j.Organizer, j => j.Participants))
                .Returns(journeys);
            unitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetJourneyById(journey?.Id ?? 0);

            result.Should().BeEquivalentTo(journey);
        }

        [Fact]
        public void TestGetJourneyById_WhenJourneyNotExist_ReturnsNull()
        {
            var journeys = fixture.Create<Journey[]>().AsQueryable();

            repository.Setup(r => r.Query(j => j.Organizer, j => j.Participants))
                .Returns(journeys);
            unitOfWork.Setup(r => r.GetRepository()).Returns(repository.Object);

            var result = journeyService.GetJourneyById(It.IsNotIn<int>(journeys.Select(j => j.Id)));

            result.Should().BeNull();
        }
    }
}
