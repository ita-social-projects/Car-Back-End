using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Filters;
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

        [Theory]
        [AutoEntityData]
        public async Task GetPastJourneys_WhenPastJourneysExist_ReturnsJourneyCollection(User user, List<JourneyModel> journeys)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneys_WhenUpcomingJourneysExist_ReturnsJourneyCollection(User user, List<JourneyModel> journeys)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneys_WhenScheduledJourneysExist_ReturnsJourneyCollection(User user, List<JourneyModel> journeys)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyById_WhenJourneyExists_ReturnsJourneyObject(JourneyModel journey)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyById_WhenJourneyNotExist_ReturnsNull(JourneyModel journey)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetRecentAddresses_WhenRecentJourneysExist_ReturnsStopCollection(User user, List<IEnumerable<StopDto>> stops)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task GetRecentAddresses_WhenRecentJourneysNotExist_ReturnsEmptyCollection(User user, List<IEnumerable<StopDto>> stops)
        {
            // Arrange
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

        [Theory]
        [AutoEntityData]
        public async Task AddJourney_WhenJourneyIsValid_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);

            journeyService.Setup(j => j.AddJourneyAsync(journeyDto))
                .ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.AddJourney(journeyDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetFiltered_ReturnsOkObjectResult(JourneyFilter filterModel, IEnumerable<ApplicantJourney> expectedResult)
        {
            // Arrange
            journeyService.Setup(j => j.GetApplicantJourneys(filterModel)).ReturnsAsync(expectedResult);

            // Act
            var result = await journeyController.GetFiltered(filterModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyIdIsValid_ReturnsOkResult(int journeyIdToDelete)
        {
            // Act
            var result = await journeyController.Delete(journeyIdToDelete);

            // Assert
            journeyService.Verify(service => service.DeleteAsync(journeyIdToDelete), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyNotExists_ThrowDbUpdateConcurrencyException(int journeyIdToDelete)
        {
            // Arrange
            journeyService.Setup(service =>
                service.DeleteAsync(journeyIdToDelete)).Throws<DbUpdateConcurrencyException>();

            // Act
            var result = journeyService.Invoking(service =>
                service.Object.DeleteAsync(journeyIdToDelete));

            // Assert
            await result.Should().ThrowAsync<DbUpdateConcurrencyException>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateRoute_WhenJourneyIsValid_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateRouteAsync(journeyDto)).ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.UpdateRoute(journeyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetails_WhenJourneyIsValid_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateDetailsAsync(journeyDto)).ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.UpdateDetails(journeyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
        }

        [Theory]
        [AutoData]
        public async Task CancelAsync_WhenJourneyExists_ReturnsOkResult(int journeyIdToCancel)
        {
            // Arrange
            journeyService.Setup(service => service.CancelAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            var result = await journeyController.CancelJourney(journeyIdToCancel);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task IsCanceled_WhenJourneyExists_ReturnsIsCancelledPropertyValue(int journeyId, bool expectedResult)
        {
            // Arrange
            journeyService.Setup(service => service.IsCanceled(It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Act
            var result = await journeyController.IsCanceled(journeyId);

            // Assert
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [AutoData]
        public async Task IsCanceled_WhenJourneyDoesntExist_ReturnsFalse(int journeyId)
        {
            // Arrange
            var expectedResult = false;
            journeyService.Setup(service => service.IsCanceled(It.IsAny<int>())).ReturnsAsync(expectedResult);

            // Act
            var result = await journeyController.IsCanceled(journeyId);

            // Assert
            (result as OkObjectResult)?.Value.Should().BeEquivalentTo(expectedResult);
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserFromJourney_ReturnsOkResult(int journeyId, int userId)
        {
            // Arrange
            journeyService.Setup(service => service.DeleteUserFromJourney(journeyId, userId)).Returns(Task.CompletedTask);

            // Act
            var result = await journeyController.DeleteUserFromJourney(journeyId, userId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}