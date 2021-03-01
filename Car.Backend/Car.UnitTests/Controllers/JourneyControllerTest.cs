using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.Journey;
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
        public async Task GetPastJourneys_WithExistingUser_ReturnsJourneyCollection()
        {
            var user = fixture.Create<User>();
            var journeys = fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetPastJourneysAsync(It.IsAny<int>()))
                .ReturnsAsync(journeys);

            var result = await journeyController.GetPast(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public async Task GetUpcomingJourneys_WithExistingUser_ReturnsJourneyCollection()
        {
            var user = fixture.Create<User>();
            var journeys = fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetUpcomingJourneysAsync(It.IsAny<int>()))
                .ReturnsAsync(journeys);

            var result = await journeyController.GetUpcoming(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public async Task GetScheduledJourneys_WithExistingUser_ReturnsJourneyCollection()
        {
            var user = fixture.Create<User>();
            var journeys = fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetScheduledJourneysAsync(It.IsAny<int>()))
                .ReturnsAsync(journeys);

            var result = await journeyController.GetScheduled(user.Id);

            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }
    }
}
