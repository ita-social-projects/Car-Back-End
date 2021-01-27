using System.Collections.Generic;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class JourneyControllerTest
    {
        private readonly Mock<IJourneyService> journeyService;
        private readonly JourneyController journeyController;
        private readonly Fixture fixture;

        public JourneyControllerTest()
        {
            journeyService = new Mock<IJourneyService>();
            journeyController = new JourneyController(journeyService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetCurrentJourney_WithExistingUser_Returns()
        {
            var user = fixture.Create<User>();
            var journey = fixture.Create<Journey>();

            journeyService.Setup(j => j.GetCurrentJourney(It.IsAny<int>()))
                .Returns(journey);

            var result = journeyController.GetCurent(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journey);
            }
        }

        [Fact]
        public void TestGetCurrentJourney_CurrentJourneyDoesntExist_Returns()
        {
            var user = fixture.Create<User>();

            journeyService.Setup(j => j.GetCurrentJourney(It.IsAny<int>()))
                .Returns((Journey)null);

            var result = journeyController.GetCurent(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public void TestGetPastJourney_WithExistingUser_Returns()
        {
            var user = fixture.Create<User>();
            var journeys = fixture.Create<List<Journey>>();

            journeyService.Setup(j => j.GetPastJourneys(It.IsAny<int>()))
                .Returns(journeys);

            var result = journeyController.GetPast(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public void TestGetUpcomingJourney_WithExistingUser_Returns()
        {
            var user = fixture.Create<User>();
            var journey = fixture.Create<List<Journey>>();

            journeyService.Setup(j => j.GetUpcomingJourneys(It.IsAny<int>()))
                .Returns(journey);

            var result = journeyController.GetUpcoming(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journey);
            }
        }

        [Fact]
        public void TestGetScheduledJourney_WithExistingUser_Returns()
        {
            var user = fixture.Create<User>();
            var journey = fixture.Create<List<Journey>>();

            journeyService.Setup(j => j.GetScheduledJourneys(It.IsAny<int>()))
                .Returns(journey);

            var result = journeyController.GetScheduled(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journey);
            }
        }
    }
}
