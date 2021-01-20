using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Controllers
{
    [Route("api/cars")]
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
        /// <param name="id">The car identifier.</param>
        /// <returns>The car entity</returns>
        [HttpGet("{id}")]
        public IActionResult GetCarById(int id)
        {
            return Ok(carService.GetCarById(id));
        }

        /// <summary>
        /// Uploads the car photo.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <param name="carFile">The car file.</param>
        /// <returns>The car entity</returns>
        [HttpPut("{id}/photo")]
        public async Task<IActionResult> UploadCarPhoto(int id, [FromForm] FormImage carFile) =>
            Ok(await imageService.UploadImage(id, carFile.Image));

        /// <summary>
        /// Deletes the car photo.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns>The car entity</returns>
        [HttpDelete("{id}/photo")]
        public async Task<IActionResult> DeleteCarPhoto(int id) => Ok(await imageService.DeleteImage(id));

        /// <summary>
        /// Gets the car file by identifier.
        /// </summary>
        /// <param name="id">The car identifier.</param>
        /// <returns>Base64 array of car photo</returns>
        [HttpGet("{id}/photo")]
        public async Task<IActionResult> GetCarFileById(int id) => Ok(await imageService.GetImageBytesById(id));
    }
}
