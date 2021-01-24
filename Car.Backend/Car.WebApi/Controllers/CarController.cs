using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.WebApi.Controllers
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
