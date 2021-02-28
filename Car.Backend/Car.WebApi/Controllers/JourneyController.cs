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
        /// Gets journey by identifier.
        /// </summary>
        /// <param name="id">Journey identifier</param>
        /// <returns>Journey</returns>
        [HttpGet("{id}")]
        public IActionResult GetJourneyById(int id) => Ok(journeyService.GetJourneyById(id));

        /// <summary>
        /// Gets recent addresses by identifier.
        /// </summary>
        /// <param name="id">Journey identifier</param>
        /// <returns>recent addresses</returns>
        [HttpGet("recent/{id}")]
        public IActionResult GetRecentAdresses(int id) => Ok(journeyService.GetStopsFromRecentJourneys(id));
    }
}
