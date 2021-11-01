using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/cars")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
        }

        /// <summary>
        /// Gets all the cars of current user
        /// </summary>
        /// <returns>All user's cars</returns>
        [HttpGet("by-user")]
        public async Task<IActionResult> GetAllByUserId() =>
             Ok(await carService.GetAllByUserIdAsync());

        /// <summary>
        /// Adds the car asynchronously.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns>New car.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCar([FromForm] CreateCarDto car) =>
            Ok(await carService.AddCarAsync(car));

        /// <summary>
        /// Gets the car by identifier.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns>The car entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCarById(int id) =>
            Ok(await carService.GetCarByIdAsync(id));

        /// <summary>
        /// Updates car.
        /// </summary>
        /// <param name="updateCarModel">The car.</param>
        /// <returns>The updated car.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCar([FromForm] UpdateCarDto updateCarModel)
        {
            var (isCarUpdated, updatedCar) = await carService.UpdateCarAsync(updateCarModel);
            return isCarUpdated ? Ok(updatedCar) : Forbid();
        }

        /// <summary>
        /// deletes car by identifier
        /// </summary>
        /// <param name="id">car Id</param>
        /// <returns>ok</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool isCarDeleted = await carService.DeleteAsync(id);
            return isCarDeleted ? Ok() : Forbid();
        }
    }
}
