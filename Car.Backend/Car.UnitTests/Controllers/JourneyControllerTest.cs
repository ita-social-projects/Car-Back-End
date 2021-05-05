using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class JourneyControllerTest : TestBase
    {
        private readonly Mock<IJourneyService> journeyService;
        private readonly JourneyController journeyController;

        public JourneyControllerTest()
        {
            journeyService = new Mock<IJourneyService>();
            journeyController = new JourneyController(journeyService.Object);
        }

        [Fact]
        public async Task GetPastJourneys_WhenPastJourneysExist_ReturnsJourneyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var journeys = Fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetPastJourneysAsync(user.Id))
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetPast(user.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public async Task GetUpcomingJourneys_WhenUpcomingJourneysExist_ReturnsJourneyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var journeys = Fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetUpcomingJourneysAsync(user.Id))
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetUpcoming(user.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public async Task GetScheduledJourneys_WhenScheduledJourneysExist_ReturnsJourneyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var journeys = Fixture.Create<List<JourneyModel>>();

            journeyService.Setup(j => j.GetScheduledJourneysAsync(user.Id))
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetScheduled(user.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Fact]
        public async Task GetJourneyById_WhenJourneyExists_ReturnsJourneyObject()
        {
            // Arrange
            var journey = Fixture.Create<JourneyModel>();

            journeyService.Setup(j => j.GetJourneyByIdAsync(journey.Id))
                .ReturnsAsync(journey);

            // Act
            var result = await journeyController.GetJourneyById(journey.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journey);
            }
        }

        [Fact]
        public async Task GetJourneyById_WhenJourneyNotExist_ReturnsNull()
        {
            // Arrange
            var journey = Fixture.Create<JourneyModel>();

            journeyService.Setup(j => j.GetJourneyByIdAsync(journey.Id))
                .ReturnsAsync((JourneyModel)null);

            // Act
            var result = await journeyController.GetJourneyById(journey.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetRecentAddresses_WhenRecentJourneysExist_ReturnsStopCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var stops = Fixture.Create<List<IEnumerable<StopDto>>>();

            journeyService.Setup(j => j.GetStopsFromRecentJourneysAsync(user.Id, 5))
                .ReturnsAsync(stops);

            // Act
            var result = await journeyController.GetRecentAddresses(user.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(stops);
            }
        }

        [Fact]
        public async Task GetRecentAddresses_WhenRecentJourneysNotExist_ReturnsEmptyCollection()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var stops = new List<IEnumerable<StopDto>>();

            journeyService.Setup(j => j.GetStopsFromRecentJourneysAsync(user.Id, 5))
                .ReturnsAsync(stops);

            // Act
            var result = await journeyController.GetRecentAddresses(user.Id);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(stops);
            }
        }

        [Fact]
        public async Task AddJourney_WhenJourneyIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var createJourneyModel = Fixture.Create<CreateJourneyModel>();
            var expectedJourney = Mapper.Map<CreateJourneyModel, JourneyModel>(createJourneyModel);

            journeyService.Setup(j => j.AddJourneyAsync(createJourneyModel))
                .ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.AddJourney(createJourneyModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
            }
        }
    }
}
