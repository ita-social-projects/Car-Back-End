using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
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
        public IActionResult GetAllByUserId(int id)
        {
            var locations = locationService.GetAllByUserId(id).ToList();

            return Ok(locations);
        }

        /// <summary>
        /// Adds the location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <returns>New location</returns>
        [HttpPost]
        public IActionResult AddLocation([FromBody] Location location)
        {
            return Ok(locationService.AddLocation(location));
        }

        /// <summary>
        /// Gets the location by identifier.
        /// </summary>
        /// <param name="id">The location identifier.</param>
        /// <returns>The location entity</returns>
        [HttpGet("{id}")]
        public IActionResult GetLocationById(int id)
        {
            return Ok(locationService.GetLocationById(id));
        }
    }
}
