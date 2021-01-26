using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService journeyService;

        public JourneyController(IJourneyService journeyService) => this.journeyService = journeyService;

        /// <summary>
        /// Looks for information about current journey where current user is participent or driver
        /// </summary>
        /// <param name="userId">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("current/{userId}")]
        public IActionResult GetCurent(int userId) => Ok(journeyService.GetCurrentJourney(userId));

        /// <summary>
        /// Looks for information about past journeys where current user is participent or driver
        /// </summary>
        /// <param name="userId">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("past/{userId}")]
        public IActionResult GetPast(int userId) => Ok(journeyService.GetPastJourneys(userId));

        /// <summary>
        /// Looks for information about all upcoming journeys where current user is participent or driver
        /// </summary>
        /// <param name="userId">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("upcoming/{userId}")]
        public IActionResult GetUpcoming(int userId) => Ok(journeyService.GetUpcomingJourneys(userId));

        /// <summary>
        /// Looks for information about all scheduled journeys where current user is participent or driver
        /// </summary>
        /// <param name="userId">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("scheduled/{userId}")]
        public IActionResult GetScheduled(int userId) => Ok(journeyService.GetScheduledJourneys(userId));
    }
}
