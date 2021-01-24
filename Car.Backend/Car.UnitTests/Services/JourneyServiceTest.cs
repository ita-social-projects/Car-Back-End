using System;
using System.Collections.Generic;
using System.Linq;
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

        public JourneyServiceTest()
        {
            repository = new Mock<IRepository<Journey>>();
            unitOfWork = new Mock<IUnitOfWork<Journey>>();

            journeyService = new JourneyService(unitOfWork.Object);
        }

        public Journey GetJourney() =>
            new Journey()
            {
                Id = It.IsAny<int>(),
                DepartureTime = DateTime.Now,
                Driver = It.IsAny<User>(),
                DriverId = It.IsAny<int>(),
                JourneyDuration = new TimeSpan(0, 15, 0),
                Participants = new List<UserJourney>() { new UserJourney() }.AsEnumerable(),
                Schedule = new Schedule(),
            };

        [Fact]
        public void TestGetCurrentJourney_ReturnsJourneyObject()
        {
            var currentJourney = GetJourney();
            var journey = new List<Journey>() { currentJourney }.AsQueryable();

            repository.Setup(repository => repository.Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver))
                .Returns(journey);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetCurrentJourney(It.IsAny<int>());

            result.Should().BeEquivalentTo(currentJourney);
        }

        [Fact]
        public void TestGetPastJourneys_ReturnsJourneyObject()
        {
            var pastJourney = GetJourney();
            pastJourney.DepartureTime = DateTime.Now.AddMinutes(-30);
            var journey = new List<Journey>() { pastJourney }.AsQueryable();

            repository.Setup(repository => repository.Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver))
                .Returns(journey);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetPastJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journey);
        }

        [Fact]
        public void TestGetUpcomingJourneys_ReturnsJourneyObject()
        {
            var upcomingJourney = GetJourney();
            upcomingJourney.DepartureTime = DateTime.Now.AddMinutes(30);
            var journey = new List<Journey>() { upcomingJourney }.AsQueryable();

            repository.Setup(repository => repository.Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver))
                .Returns(journey);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetUpcomingJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journey);
        }

        [Fact]
        public void TestGetScheduledJourneys_ReturnsJourneyObject()
        {
            var scheduledJourney = GetJourney();
            var journey = new List<Journey>() { scheduledJourney }.AsQueryable();

            repository.Setup(repository => repository.Query(
                    journeyStops => journeyStops.UserStops,
                    driver => driver.Driver))
                .Returns(journey);

            unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(repository.Object);

            var result = journeyService.GetScheduledJourneys(It.IsAny<int>());

            result.Should().BeEquivalentTo(journey);
        }
    }
}
