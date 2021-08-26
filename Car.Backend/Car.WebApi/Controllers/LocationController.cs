using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Dto.Location;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService locationService;

        public LocationController(ILocationService locationService)
        {
            this.locationService = locationService;
        }

        /// <summary>
        /// Gets all the locations of current user
        /// </summary>
        /// <returns>All user's locations</returns>
        [HttpGet("by-user")]
        public async Task<IActionResult> GetAllByUserId() =>
            Ok(await locationService.GetAllByUserIdAsync());

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>New location</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] LocationDto location) =>
            Ok(await locationService.AddLocationAsync(location));

        /// <summary>
        /// updates location
        /// </summary>
        /// <param name="location">location to be updated</param>
        /// <returns>updated location</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateLocationDto location) =>
            Ok(await locationService.UpdateAsync(location));

        /// <summary>
        /// Gets the location by identifier.
        /// </summary>
        /// <param name="id">The location identifier.</param>
        /// <returns>The location entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await locationService.GetLocationByIdAsync(id));

        /// <summary>
        /// deletes location by identifier
        /// </summary>
        /// <param name="id">location Id</param>
        /// <returns>ok</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool isLocationDeleted = await locationService.DeleteAsync(id);
            return isLocationDeleted ? Ok() : Forbid();
        }
    }
}
