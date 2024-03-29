﻿using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Journey;
using Car.Domain.Filters;
using Car.Domain.Models.User;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/journeys")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService journeyService;

        public JourneyController(IJourneyService journeyService) =>
            this.journeyService = journeyService;

        /// <summary>
        /// Looks for information about past journeys where current user is participant or driver
        /// </summary>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("past")]
        public async Task<IActionResult> GetPast() =>
            Ok(await journeyService.GetPastJourneysAsync());

        /// <summary>
        /// Looks for information about all upcoming journeys where current user is participant or driver
        /// </summary>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("upcoming")]
        public async Task<IActionResult> GetUpcoming() =>
            Ok(await journeyService.GetUpcomingJourneysAsync());

        /// <summary>
        /// Looks for information about all scheduled journeys where current user is participant or driver
        /// </summary>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("scheduled")]
        public async Task<IActionResult> GetScheduled() =>
            Ok(await journeyService.GetScheduledJourneysAsync());

        /// <summary>
        /// Gets journey by identifier.
        /// </summary>
        /// <param name="id">Journey identifier</param>
        /// <param name="isJourneyCanceled">Include journey cancelation status</param>
        /// <returns>Journey</returns>
        [HttpGet("{id}/{isJourneyCanceled}")]
        public async Task<IActionResult> GetJourneyById(int id, bool isJourneyCanceled = false) =>
            Ok(await journeyService.GetJourneyByIdAsync(id, isJourneyCanceled));

        /// <summary>
        /// Gets recent addresses by identifier.
        /// </summary>
        /// <returns>recent addresses</returns>
        [HttpGet("recent")]
        public async Task<IActionResult> GetRecentAddresses() =>
            Ok(await journeyService.GetStopsFromRecentJourneysAsync());

        /// <summary>
        /// Gets canceled journey.
        /// </summary>
        /// <returns>canceled journeys</returns>
        [HttpGet("canceled")]
        public async Task<IActionResult> GetCanceled() =>
            Ok(await journeyService.GetCanceledJourneysAsync());

        /// <summary>
        /// Gets requested journey.
        /// </summary>
        /// <returns>requested journeys</returns>
        [HttpGet("requested")]
        public async Task<IActionResult> GetRequested() =>
            Ok(await journeyService.GetRequestedJourneysAsync());

        /// <summary>
        /// Adds the journey asynchronously.
        /// </summary>
        /// <param name="journeyDto">The journey model.</param>
        /// <returns>Added journey.</returns>
        [HttpPost]
        public async Task<IActionResult> AddJourney([FromBody] JourneyDto journeyDto)
        {
            var addedJourney = await journeyService.AddJourneyAsync(journeyDto);

            return addedJourney.IsDepartureTimeValid ? Ok(addedJourney) : Forbid();
        }

        /// <summary>
        /// Adds the scheduled journey asynchronously.
        /// </summary>
        /// /// <param name="scheduleDto">The schedule model.</param>
        /// <returns>Added scheduled journey.</returns>
        [HttpPost("scheduled-journey")]
        public async Task<IActionResult> AddScheduledJourney([FromBody] ScheduleDto scheduleDto)
        {
            var addedJourney = await journeyService.AddScheduledJourneyAsync(scheduleDto);

            return addedJourney.IsDepartureTimeValid ? Ok(addedJourney) : Forbid();
        }

        /// <summary>
        /// Adds the scheduleDto asynchronously.
        /// </summary>
        /// <param name="journeyDto">The journey model.</param>
        /// <returns>Added schedule.</returns>
        [HttpPost("schedule")]
        public async Task<IActionResult> AddSchedule([FromBody] JourneyDto journeyDto)
        {
            var addedSchedule = await journeyService.AddScheduleAsync(journeyDto);

            return addedSchedule.IsDepartureTimeValid ? Ok(addedSchedule) : Forbid();
        }

        /// <summary>
        /// Returns journeys filtered by given conditions.
        /// </summary>
        /// <param name="journeyFilterModel">Model that contains needed parameters to filter by</param>
        /// <returns>Collection of filtered journeys.</returns>
        [HttpGet("filter/")]
        public async Task<IActionResult> GetFiltered([FromQuery] JourneyFilter journeyFilterModel) =>
            Ok(await journeyService.GetApplicantJourneysAsync(journeyFilterModel));

        /// <summary>
        /// deletes journey by identifier
        /// </summary>
        /// <param name="id">journey Id</param>
        /// <returns>OkResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isJourneyDeleted = await journeyService.DeleteAsync(id);
            return isJourneyDeleted ? Ok() : Forbid();
        }

        /// <summary>
        /// Update the journey route asynchronously.
        /// </summary>
        /// <param name="journey">The journey dto.</param>
        /// <returns>OkResult</returns>
        [HttpPut("update-route")]
        public async Task<IActionResult> UpdateRoute([FromBody] JourneyDto journey)
        {
            var (isJourneyUpdated, updatedJourney) = await journeyService.UpdateRouteAsync(journey);
            return isJourneyUpdated ? Ok(updatedJourney) : Forbid();
        }

        /// <summary>
        /// Update the journey details asynchronously.
        /// </summary>
        /// <param name="journey">The journey dto.</param>
        /// <returns>OkResult</returns>
        [HttpPut("update-details")]
        public async Task<IActionResult> UpdateDetails([FromBody] JourneyDto journey)
        {
            var (isJourneyUpdated, updatedJourney) = await journeyService.UpdateDetailsAsync(journey);
            return isJourneyUpdated ? Ok(updatedJourney) : Forbid();
        }

        /// <summary>
        /// Update the journey invitation asynchronously.
        /// </summary>
        /// <param name="invitation">The journey dto.</param>
        /// <returns>OkResult</returns>
        [HttpPut("update-invitation")]
        public async Task<IActionResult> UpdateInvitation([FromBody] InvitationDto invitation)
        {
            var (isInvitationUpdated, updatedInvitation) = await journeyService.UpdateInvitationAsync(invitation);
            return isInvitationUpdated ? Ok(updatedInvitation) : Forbid();
        }

        /// <summary>
        /// Cancels journey
        /// </summary>
        /// <param name="id">id of journey that should be cancelled</param>
        /// <returns>OkResult</returns>
        [HttpPut("cancel/{id}")]
        public async Task<IActionResult> CancelJourney(int id)
        {
            var isCancelled = await journeyService.CancelAsync(id);
            return isCancelled ? Ok() : Forbid();
        }

        /// <summary>
        /// Defines if journey was canceled
        /// </summary>
        /// <param name="id">id of journey</param>
        /// <returns>bool value that indicates whether journey was canceled</returns>
        [HttpGet("is-canceled/{id}")]
        public async Task<IActionResult> IsCanceled(int id) =>
            Ok(await journeyService.IsCanceled(id));

        /// <summary>
        /// Deletes user from journey
        /// </summary>
        /// <param name="journeyId">journey Id</param>
        /// <param name="userId">user Id</param>
        /// <returns>OkResult</returns>
        [HttpDelete("delete-user/{journeyId}/{userId}")]
        public async Task<IActionResult> DeleteUserFromJourney(int journeyId, int userId)
        {
            bool isUserDeletedFromJourney = await journeyService.DeleteUserFromJourney(journeyId, userId);
            return isUserDeletedFromJourney ? Ok() : Forbid();
        }

        /// <summary>
        /// Adds user to journey
        /// </summary>
        /// <param name="applyModel">Applicant Apply Model</param>
        /// <returns>OkResult</returns>
        [HttpPut("add-user/")]
        public async Task<IActionResult> AddUserToJourney([FromBody] ApplicantApplyModel applyModel)
        {
            var (isAddingAllowed, isUserAdded) = await journeyService.AddUserToJourney(applyModel);
            return isAddingAllowed ? Ok(isUserAdded) : Forbid();
        }

        [HttpGet("journey-user/{journeyId}/{userId}/{isJourneyCanceled}")]
        public async Task<IActionResult> GetJourneyWithJourneyUser(
            int journeyId,
            int userId,
            bool isJourneyCanceled = false)
        {
            var journeyWithUser =
                await journeyService.GetJourneyWithJourneyUserByIdAsync(journeyId, userId, isJourneyCanceled);
            return Ok(journeyWithUser);
        }
    }
}
