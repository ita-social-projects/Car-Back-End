using System;
using System.Collections.Generic;
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

        public JourneyControllerTest()
        {
            this.journeyService = new Mock<IJourneyService>();
            this.journeyController = new JourneyController(this.journeyService.Object);
        }

        public User GetCurrentUser()
        {
            return new User()
            {
                Id = It.IsAny<int>(),
                Email = It.IsAny<string>(),
                HireDate = It.IsAny<DateTime>(),
                ImageId = It.IsAny<string>(),
                Name = It.IsAny<string>(),
                Location = It.IsAny<string>(),
                Position = It.IsAny<string>(),
            };
        }

        [Fact]
        public void TestGetCurrentJourney_WithExistingUser_Returns()
        {
            var user = this.GetCurrentUser();
            var journey = new Journey();

            this.journeyService.Setup(journey => journey.GetCurrentJourney(It.IsAny<int>()))
                .Returns(journey);

            var result = this.journeyController.GetCurent(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult).StatusCode.Should().Be(200);
                (result as OkObjectResult).Value.Should().Be(journey);
            }
        }

        [Fact]
        public void TestGetCurrentJourney_CurrentJourneyDoesntExist_Returns()
        {
            var user = this.GetCurrentUser();
            this.journeyService.Setup(journey => journey.GetCurrentJourney(It.IsAny<int>()))
                .Returns((Journey)null);

            var result = this.journeyController.GetCurent(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult).StatusCode.Should().Be(200);
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }

        [Fact]
        public void TestGetPastJourney_WithExistingUser_Returns()
        {
            var user = this.GetCurrentUser();
            var journey = new List<Journey>() { new Journey() };

            this.journeyService.Setup(journey => journey.GetPastJourneys(It.IsAny<int>()))
                .Returns(journey);

            var result = this.journeyController.GetPast(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult).StatusCode.Should().Be(200);
                (result as OkObjectResult).Value.Should().Be(journey);
            }
        }

        [Fact]
        public void TestGetUpcomingJourney_WithExistingUser_Returns()
        {
            var user = this.GetCurrentUser();
            var journey = new List<Journey>() { new Journey() };

            this.journeyService.Setup(journey => journey.GetUpcomingJourneys(It.IsAny<int>()))
                .Returns(journey);

            var result = this.journeyController.GetUpcoming(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult).StatusCode.Should().Be(200);
                (result as OkObjectResult).Value.Should().Be(journey);
            }
        }

        [Fact]
        public void TestGetScheduledJourney_WithExistingUser_Returns()
        {
            var user = this.GetCurrentUser();
            var journey = new List<Journey>() { new Journey() };

            this.journeyService.Setup(journey => journey.GetScheduledJourneys(It.IsAny<int>()))
                .Returns(journey);

            var result = this.journeyController.GetScheduled(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult).StatusCode.Should().Be(200);
                (result as OkObjectResult).Value.Should().Be(journey);
            }
        }
    }
}
