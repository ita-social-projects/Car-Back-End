using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
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
        private readonly Mock<IUnitOfWork<Journey>> journeyUnitOfWork;
        private readonly Mock<IUnitOfWork<User>> userUnitOfWork;
        private readonly Fixture fixture;

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            (journeyUnitOfWork, userUnitOfWork) = (new Mock<IUnitOfWork<Journey>>(), new Mock<IUnitOfWork<User>>());

            journeyService = new JourneyService(journeyUnitOfWork.Object, userUnitOfWork.Object);

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
                JourneyDuration = new TimeSpan(0, 15, 0),
                Participants = fixture.Create<List<User>>(),
                Schedule = fixture.Create<Schedule>(),
            };

        [Fact]
        public void TestGetCurrentJourney_ReturnsJourneyObject()
        {
            var currentJourney = GetJourney();

            var journeys = new List<Journey> { currentJourney }.AsQueryable();

            repository.Setup(r => r.Query(
                    journeyStops => journeyStops.Stops,
                    driver => driver.Organizer))
                .Returns(journeys);

            journeyUnitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(It.IsAny<int>());

            result.Should().BeEquivalentTo(currentJourney);
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

            journeyUnitOfWork.Setup(r => r.GetRepository())
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

            journeyUnitOfWork.Setup(r => r.GetRepository())
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

            journeyUnitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetScheduledJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journeys);
        }
    }
}
