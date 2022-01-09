using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Journey;
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
        public async Task GetPastJourneys_WhenPastJourneysExist_ReturnsJourneyCollection(List<JourneyModel> journeys)
        {
            // Arrange
            journeyService.Setup(j => j.GetPastJourneysAsync())
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetPast();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUpcomingJourneys_WhenUpcomingJourneysExist_ReturnsJourneyCollection(List<JourneyModel> journeys)
        {
            // Arrange
            journeyService.Setup(j => j.GetUpcomingJourneysAsync())
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetUpcoming();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetScheduledJourneys_WhenScheduledJourneysExist_ReturnsJourneyCollection(List<JourneyModel> journeys)
        {
            // Arrange
            journeyService.Setup(j => j.GetScheduledJourneysAsync())
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetScheduled();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetCanceledJourneysAsync_WhenCanceledJourneysExist_ReturnsJourneyCollection(List<JourneyModel> journeys)
        {
            // Arrange
            journeyService.Setup(j => j.GetCanceledJourneysAsync())
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetCanceled();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRequestedJourneysAsync_WhenRequestedJourneysExist_ReturnsJourneyCollection(List<RequestDto> journeys)
        {
            // Arrange
            journeyService.Setup(j => j.GetRequestedJourneysAsync())
                .ReturnsAsync(journeys);

            // Act
            var result = await journeyController.GetRequested();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(journeys);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyById_WhenJourneyExists_ReturnsJourneyObject(JourneyModel journey, bool withCancelledStops)
        {
            // Arrange
            withCancelledStops = false;
            journeyService.Setup(j => j.GetJourneyByIdAsync(journey.Id, withCancelledStops))
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
        public async Task GetJourneyById_WhenJourneyNotExist_ReturnsNull(JourneyModel journey, bool withCancelledStops = false)
        {
            // Arrange
            journeyService.Setup(j => j.GetJourneyByIdAsync(journey.Id, withCancelledStops))
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
        public async Task GetRecentAddresses_WhenRecentJourneysExist_ReturnsStopCollection(List<IEnumerable<StopDto>> stops)
        {
            // Arrange
            journeyService.Setup(j => j.GetStopsFromRecentJourneysAsync(5))
                .ReturnsAsync(stops);

            // Act
            var result = await journeyController.GetRecentAddresses();

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().Be(stops);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task GetRecentAddresses_WhenRecentJourneysNotExist_ReturnsEmptyCollection(List<IEnumerable<StopDto>> stops)
        {
            // Arrange
            journeyService.Setup(j => j.GetStopsFromRecentJourneysAsync(5))
                .ReturnsAsync(stops);

            // Act
            var result = await journeyController.GetRecentAddresses();

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
            var journeyModel = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);

            var expectedJourney = new JourneyTimeModel { JourneyModel = journeyModel, IsDepartureTimeValid = true };

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
        public async Task AddJourney_WhenJourneyIsInValid_ReturnsForbidResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = new JourneyTimeModel { JourneyModel = null, IsDepartureTimeValid = false };

            journeyService.Setup(j => j.AddJourneyAsync(journeyDto))
                .ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.AddJourney(journeyDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<ForbidResult>();
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddSchedule_WhenScheduleIsValid_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var journeyModel = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            var scheduleModel = Mapper.Map<Schedule, ScheduleModel>(journeyModel.Schedule);

            var expectedSchedule = new ScheduleTimeModel() { ScheduleModel = scheduleModel, IsDepartureTimeValid = true };

            journeyService.Setup(j => j.AddScheduleAsync(journeyDto))
                .ReturnsAsync(expectedSchedule);

            // Act
            var result = await journeyController.AddSchedule(journeyDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedSchedule);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddSchedule_WhenScheduleIsInValid_ReturnsForbidResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedSchedule = new ScheduleTimeModel() { ScheduleModel = null, IsDepartureTimeValid = false };

            journeyService.Setup(j => j.AddScheduleAsync(journeyDto))
                .ReturnsAsync(expectedSchedule);

            // Act
            var result = await journeyController.AddSchedule(journeyDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<ForbidResult>();
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddScheduledJourney_WhenJourneyIsValid_ReturnsOkObjectResult(ScheduleDto scheduleDto)
        {
            // Arrange
            var schedule = Mapper.Map<ScheduleDto, Schedule>(scheduleDto);
            var journeyModel = Mapper.Map<Journey, JourneyModel>(schedule.Journey);

            var expectedJourney = new JourneyTimeModel() { JourneyModel = journeyModel, IsDepartureTimeValid = true };

            journeyService.Setup(j => j.AddScheduledJourneyAsync(scheduleDto))
                .ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.AddScheduledJourney(scheduleDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddScheduledJourney_WhenJourneyIsInValid_ReturnsForbidResult(ScheduleDto scheduleDto)
        {
            // Arrange
            var expectedJourney = new JourneyTimeModel() { JourneyModel = null, IsDepartureTimeValid = false };

            journeyService.Setup(j => j.AddScheduledJourneyAsync(scheduleDto))
                .ReturnsAsync(expectedJourney);

            // Act
            var result = await journeyController.AddScheduledJourney(scheduleDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<ForbidResult>();
            }
        }

        [Theory]
        [AutoEntityData]
        public void GetFiltered_ReturnsOkObjectResult(JourneyFilter filterModel, IEnumerable<ApplicantJourney> expectedResult)
        {
            // Arrange
            journeyService.Setup(j => j.GetApplicantJourneys(filterModel)).Returns(expectedResult);

            // Act
            var result = journeyController.GetFiltered(filterModel);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedResult);
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyIdIsValidAndIsAllowed_ReturnsOkResult(int journeyIdToDelete)
        {
            // Arrange
            journeyService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await journeyController.Delete(journeyIdToDelete);

            // Assert
            journeyService.Verify(service => service.DeleteAsync(journeyIdToDelete), Times.Once());
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteAsync_WhenJourneyIdIsValidAndIsForbidden_ReturnsOkResult(int journeyIdToDelete)
        {
            // Arrange
            journeyService.Setup(service => service.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await journeyController.Delete(journeyIdToDelete);

            // Assert
            journeyService.Verify(service => service.DeleteAsync(journeyIdToDelete), Times.Once());
            result.Should().BeOfType<ForbidResult>();
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
        public async Task UpdateRoute_WhenJourneyIsValidAndUserIsOrganizer_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateRouteAsync(journeyDto)).ReturnsAsync((true, expectedJourney));

            // Act
            var result = await journeyController.UpdateRoute(journeyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateRoute_WhenJourneyIsValidAndUserIsNotOrganizer_ReturnsForbidObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateRouteAsync(journeyDto)).ReturnsAsync((false, null));

            // Act
            var result = await journeyController.UpdateRoute(journeyDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetails_WhenJourneyIsValidAndUserIsOrganizer_ReturnsOkObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateDetailsAsync(journeyDto)).ReturnsAsync((true, expectedJourney));

            // Act
            var result = await journeyController.UpdateDetails(journeyDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedJourney);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateDetails_WhenJourneyIsValidAndUserIsNotOrganizer_ReturnsForbidObjectResult(JourneyDto journeyDto)
        {
            // Arrange
            var expectedJourney = Mapper.Map<JourneyDto, JourneyModel>(journeyDto);
            journeyService.Setup(service =>
                service.UpdateDetailsAsync(journeyDto)).ReturnsAsync((false, null));

            // Act
            var result = await journeyController.UpdateDetails(journeyDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateInvitations_WhenInvitationIsValidAndIsAllowed_ReturnsOkObjectResult(
            InvitationDto invitationDto,
            InvitationDto expectedInvitationDto)
        {
            // Arrange
            journeyService.Setup(service =>
                service.UpdateInvitationAsync(invitationDto)).ReturnsAsync((true, expectedInvitationDto));

            // Act
            var result = await journeyController.UpdateInvitation(invitationDto);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            (result as OkObjectResult)?.Value.Should().Be(expectedInvitationDto);
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateInvitations_WhenInvitationIsValidAndIsNotAllowed_ReturnsForbidObjectResult(InvitationDto invitationDto)
        {
            // Arrange
            journeyService.Setup(service =>
                service.UpdateInvitationAsync(invitationDto)).ReturnsAsync((false, null));

            // Act
            var result = await journeyController.UpdateInvitation(invitationDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoData]
        public async Task CancelAsync_WhenJourneyExistsAndUserIsOrganizer_ReturnsOkResult(int journeyIdToCancel)
        {
            // Arrange
            journeyService.Setup(service => service.CancelAsync(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await journeyController.CancelJourney(journeyIdToCancel);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task CancelAsync_WhenJourneyExistsAndUserIsNotOrganizer_ReturnsForbidResult(int journeyIdToCancel)
        {
            // Arrange
            journeyService.Setup(service => service.CancelAsync(It.IsAny<int>())).ReturnsAsync(false);

            // Act
            var result = await journeyController.CancelJourney(journeyIdToCancel);

            // Assert
            result.Should().BeOfType<ForbidResult>();
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
        public async Task DeleteUserFromJourney_IsAllowed_ReturnsOkResult(int journeyId, int userId)
        {
            // Arrange
            journeyService.Setup(service => service.DeleteUserFromJourney(journeyId, userId)).ReturnsAsync(true);

            // Act
            var result = await journeyController.DeleteUserFromJourney(journeyId, userId);

            // Assert
            result.Should().BeOfType<OkResult>();
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserFromJourney_IsForbidden_ReturnsOkResult(int journeyId, int userId)
        {
            // Arrange
            journeyService.Setup(service => service.DeleteUserFromJourney(journeyId, userId)).ReturnsAsync(false);

            // Act
            var result = await journeyController.DeleteUserFromJourney(journeyId, userId);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserToJourney_IsAllowed_ReturnsOkObjectResult(JourneyApplyModel journeyApply, bool expectedResult)
        {
            // Arrange
            journeyService
                .Setup(service => service.AddUserToJourney(It.IsAny<JourneyApplyModel>()))
                .ReturnsAsync((true, expectedResult));

            // Act
            var result = await journeyController.AddUserToJourney(journeyApply);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().BeEquivalentTo(expectedResult);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserToJourney_IsNotAllowed_ReturnsForbidObjectResult(JourneyApplyModel journeyApply, bool expectedResult)
        {
            // Arrange
            journeyService
                .Setup(service => service.AddUserToJourney(It.IsAny<JourneyApplyModel>()))
                .ReturnsAsync((false, expectedResult));

            // Act
            var result = await journeyController.AddUserToJourney(journeyApply);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task GetJourneyWithJourneyUser_ReturnsOkObjectResult(int journeyId, int userId, bool withCancelledStops, (JourneyModel Journey, JourneyUserDto JourneyUser) journeyWithUser)
        {
            // Arrange
            journeyService
                .Setup(service => service.GetJourneyWithJourneyUserByIdAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<bool>()))
                .ReturnsAsync(journeyWithUser);

            // Act
            var result = await journeyController.GetJourneyWithJourneyUser(journeyId, userId, withCancelledStops);

            // Assert
            using (new AssertionScope())
            {
                (result as OkObjectResult)?.StatusCode.Should().Be(200);
                (result as OkObjectResult)?.Value.Should().BeEquivalentTo(journeyWithUser);
            }
        }
    }
}