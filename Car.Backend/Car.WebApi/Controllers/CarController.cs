using System.Threading.Tasks;
using Car.Domain.Models.Car;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
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
        /// Gets all the cars by user id
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>All user's cars</returns>
        [HttpGet("by-user/{id}")]
        public async Task<IActionResult> GetAllByUserId(int id) =>
            Ok(await carService.GetAllByUserIdAsync(id));

        /// <summary>
        /// Adds the car asynchronously.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns>New car.</returns>
        [HttpPost]
        public async Task<IActionResult> AddCar([FromForm] CreateCarModel car) =>
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
        public async Task<IActionResult> UpdateCar([FromForm] UpdateCarModel updateCarModel) =>
            Ok(await carService.UpdateCarAsync(updateCarModel));

        /// <summary>
        /// deletes car by identifier
        /// </summary>
        /// <param name="id">car Id</param>
        /// <returns>ok</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
           await carService.DeleteAsync(id);
           return Ok();
        }
    }
}
