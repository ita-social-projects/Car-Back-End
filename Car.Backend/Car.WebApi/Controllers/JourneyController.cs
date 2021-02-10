using System;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/journeys")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService journeyService;

        public JourneyController(IJourneyService journeyService) => this.journeyService = journeyService;

        /// <summary>
        /// Looks for information about current journey where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("current/{id}")]
        public IActionResult GetCurrent(int id) => Ok(journeyService.GetCurrentJourney(id));

        /// <summary>
        /// Looks for information about past journeys where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("past/{id}")]
        public IActionResult GetPast(int id) => Ok(journeyService.GetPastJourneys(id));

        /// <summary>
        /// Looks for information about all upcoming journeys where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("upcoming/{id}")]
        public IActionResult GetUpcoming(int id) => Ok(journeyService.GetUpcomingJourneys(id));

        /// <summary>
        /// Looks for information about all scheduled journeys where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("scheduled/{id}")]
        public IActionResult GetScheduled(int id) => Ok(journeyService.GetScheduledJourneys(id));

        /// <summary>
        /// Adds a new participant to the Journey
        /// </summary>
        /// <param name="journeyId">id of the journey</param>
        /// <param name="userId">id of the user</param>
        /// <param name="hasLuggage">does the user has the luggage</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpPost("add-participant/journey={journeyId}&userId={userId}&hasLuggage={hasLuggage}")]
        public IActionResult PostAddParticipantAsync(int journeyId, int userId, bool hasLuggage = false) =>
            Ok(journeyService.PostApproveApplicantAsync(journeyId, userId, hasLuggage));
    }
}
