using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;
        private readonly IImageService<Data.Entities.Car, File> imageService;

        public CarController(
            ICarService carService,
            IImageService<Data.Entities.Car, File> imageService)
        {
            this.carService = carService;
            this.imageService = imageService;
        }

        /// <summary>
        /// Gets all the cars by user id
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>All user's cars</returns>
        [HttpGet("byUser/{userId}")]
        public async Task<IActionResult> GetAllByUserId(int userId)
        {
            var cars = carService.GetAllByUserId(userId).ToList();

            var listOperation = new List<Task<string>>();

            cars.ForEach(car => listOperation.Add(imageService.GetImageBytesById(car.Id)));

            await Task.WhenAll(listOperation);

            for (int i = 0; i < cars.Count; i++)
            {
                cars[i].ByteOfImage = listOperation[i].Result;
            }

            return Ok(cars);
        }

        /// <summary>
        /// Adds the car.
        /// </summary>
        /// <param name="car">The car.</param>
        /// <returns>New car</returns>
        [HttpPost]
        public IActionResult AddCar([FromBody] CarDTO car)
        {
            return Ok(carService.AddCar(car));
        }

        /// <summary>
        /// Gets the car by identifier.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <returns>The car entity</returns>
        [HttpGet("{carId}")]
        public IActionResult GetCarById(int carId)
        {
            return Ok(carService.GetCarById(carId));
        }

        /// <summary>
        /// Uploads the car photo.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <param name="carFile">The car file.</param>
        /// <returns>The car entity</returns>
        [HttpPut("{carId}/photo")]
        public async Task<IActionResult> UploadCarPhoto(int carId, [FromForm] FormImage carFile) =>
            Ok(await imageService.UploadImage(carId, carFile.Image));

        /// <summary>
        /// Deletes the car photo.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <returns>The car entity</returns>
        [HttpDelete("{carId}/photo")]
        public async Task<IActionResult> DeleteCarPhoto(int carId) => Ok(await imageService.DeleteImage(carId));

        /// <summary>
        /// Gets the car file by identifier.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <returns>Base64 array of car photo</returns>
        [HttpGet("{carId}/photo")]
        public async Task<IActionResult> GetCarFileById(int carId) => Ok(await imageService.GetImageBytesById(carId));
    }
}
