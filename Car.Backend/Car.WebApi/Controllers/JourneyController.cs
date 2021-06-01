using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Models.Journey;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
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
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("past/{id}")]
        public async Task<IActionResult> GetPast(int id) =>
            Ok(await journeyService.GetPastJourneysAsync(id));

        /// <summary>
        /// Looks for information about all upcoming journeys where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("upcoming/{id}")]
        public async Task<IActionResult> GetUpcoming(int id) =>
            Ok(await journeyService.GetUpcomingJourneysAsync(id));

        /// <summary>
        /// Looks for information about all scheduled journeys where current user is participant or driver
        /// </summary>
        /// <param name="id">id of current user</param>
        /// <returns>status of request with appropriate data</returns>
        [HttpGet("scheduled/{id}")]
        public async Task<IActionResult> GetScheduled(int id) =>
            Ok(await journeyService.GetScheduledJourneysAsync(id));

        /// <summary>
        /// Gets journey by identifier.
        /// </summary>
        /// <param name="id">Journey identifier</param>
        /// <returns>Journey</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJourneyById(int id) =>
            Ok(await journeyService.GetJourneyByIdAsync(id));

        /// <summary>
        /// Gets recent addresses by identifier.
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>recent addresses</returns>
        [HttpGet("recent/{id}")]
        public async Task<IActionResult> GetRecentAddresses(int id) =>
            Ok(await journeyService.GetStopsFromRecentJourneysAsync(id));

        /// <summary>
        /// Adds the journey asynchronously.
        /// </summary>
        /// <param name="journeyDto">The journey model.</param>
        /// <returns>Added journey.</returns>
        [HttpPost]
        public async Task<IActionResult> AddJourney([FromBody] JourneyDto journeyDto) =>
            Ok(await journeyService.AddJourneyAsync(journeyDto));

        /// <summary>
        /// Returns journeys filtered by given conditions.
        /// </summary>
        /// <param name="journeyFilterModel">Model that contains needed parameters to filter by</param>
        /// <returns>Collection of filtered journeys.</returns>
        [HttpGet("filter/")]
        public async Task<IActionResult> GetFiltered([FromQuery] JourneyFilterModel journeyFilterModel) =>
            Ok(await journeyService.GetFilteredJourneys(journeyFilterModel));

        /// <summary>
        /// deletes journey by identifier
        /// </summary>
        /// <param name="id">journey Id</param>
        /// <returns>OkResult</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await journeyService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Update the journey route asynchronously.
        /// </summary>
        /// <param name="journey">The journey dto.</param>
        /// <returns>OkResult</returns>
        [HttpPut("update-route")]
        public async Task<IActionResult> UpdateRoute([FromBody] JourneyDto journey) =>
            Ok(await journeyService.UpdateRouteAsync(journey));

        /// <summary>
        /// Update the journey details asynchronously.
        /// </summary>
        /// <param name="journey">The journey dto.</param>
        /// <returns>OkResult</returns>
        [HttpPut("update-details")]
        public async Task<IActionResult> UpdateDetails([FromBody] JourneyDto journey) =>
            Ok(await journeyService.UpdateDetailsAsync(journey));
    }
}
