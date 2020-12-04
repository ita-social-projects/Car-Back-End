using System.Threading.Tasks;
using Car.BLL.Dto;
using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService carService;

        public CarController(ICarService carService)
        {
            this.carService = carService;
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
        public async Task<IActionResult> UploadCarPhoto(int carId, [FromForm] FormImage carFile)
        {
            return Ok(await carService.UploadCarPhoto(carId, carFile.image));
        }

        /// <summary>
        /// Deletes the car photo.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <param name="carFileId">The car file identifier.</param>
        /// <returns>The car entity</returns>
        [HttpDelete("{carId}/photo/{carFileId}")]
        public async Task<IActionResult> DeleteCarPhoto(int carId, string carFileId)
        {
            return Ok(await carService.DeleteCarAvatar(carId, carFileId));
        }

        /// <summary>
        /// Gets the car file by identifier.
        /// </summary>
        /// <param name="carId">The car identifier.</param>
        /// <returns>Base64 array of car photo</returns>
        [HttpGet("photo/{carId}")]
        public async Task<IActionResult> GetCarFileById(int carId)
        {
            return Ok(await carService.GetCarFileBytesById(carId));
        }
    }
}
