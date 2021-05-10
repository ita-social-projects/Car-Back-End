using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Models.Location;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
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
        /// Gets all the locations by user id
        /// </summary>
        /// <param name="id">The location identifier.</param>
        /// <returns>All user's locations</returns>
        [HttpGet("by-user/{id}")]
        public async Task<IActionResult> GetAllByUserId(int id) =>
            Ok(await locationService.GetAllByUserIdAsync(id));

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>New location</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLocationModel location) =>
            Ok(await locationService.AddLocationAsync(location));

        /// <summary>
        /// Gets the location by identifier.
        /// </summary>
        /// <param name="id">The location identifier.</param>
        /// <returns>The location entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocationById(int id) =>
            Ok(await locationService.GetLocationByIdAsync(id));
    }
}
