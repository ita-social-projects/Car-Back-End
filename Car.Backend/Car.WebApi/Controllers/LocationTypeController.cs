using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/location-types")]
    [ApiController]
    public class LocationTypeController : ControllerBase
    {
        private readonly ILocationTypeService locationTypeService;

        public LocationTypeController(ILocationTypeService locationTypeService)
        {
            this.locationTypeService = locationTypeService;
        }

        /// <summary>
        /// Gets all location types.
        /// </summary>
        /// <returns>All the models</returns>
        [HttpGet("location-types/")]
        public IActionResult GetAll()
        {
            return Ok(locationTypeService.GetAllLocationTypes());
        }
    }
}
