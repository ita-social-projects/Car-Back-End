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
using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task GetFiltered_ReturnsOkObjectResult()
        {
            // Arrange
            var filterModel = Fixture.Create<JourneyFilterModel>();
            var journeys = Fixture.Create<IEnumerable<Journey>>();
            var expectedResult = Mapper.Map<IEnumerable<Journey>, IEnumerable<JourneyModel>>(journeys);

            journeyService.Setup(j => j.GetFilteredJourneys(filterModel)).ReturnsAsync(expectedResult);

            // Act
            var result = await journeyController.GetFiltered(filterModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Fact]
        public async Task DeleteAsync_WhenJourneyIdIsValid_ReturnsOkResult()
        {
            // Arrange
            var journeyIdToDelete = Fixture.Create<int>();

            // Act
            var result = await journeyController.Delete(journeyIdToDelete);

            // Assert
            journeyService.Verify(service => service.DeleteAsync(journeyIdToDelete), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task DeleteAsync_WhenJourneyNotExists_ThrowDbUpdateConcurrencyException()
        {
            // Arrange
            var journeyIdToDelete = Fixture.Create<int>();
            journeyService.Setup(service =>
                service.DeleteAsync(journeyIdToDelete)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = journeyService.Invoking(service =>
                service.Object.DeleteAsync(journeyIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Fact]
        public async Task UpdateCar_WhenCarIsValid_ReturnsOkObjectResult()
        {
            // Arrange
            var journeyDto = Fixture.Create<JourneyDto>();
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateAsync(journeyDto)).ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.Update(journeyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
        }
    }
}
