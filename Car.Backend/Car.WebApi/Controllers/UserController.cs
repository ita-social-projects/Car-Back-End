using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IImageService<User, File> imageService;

        public UserController(
            IUserService userService,
            IImageService<User, File> imageService)
        {
            this.userService = userService;
            this.imageService = imageService;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) => Ok(userService.GetUserById(id));

        /// <summary>
        /// Uploads the user avatar.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <param name="userFile">The user file.</param>
        /// <returns>The user entity</returns>
        [HttpPut("{id}/avatar")]
        public async Task<IActionResult> UploadUserAvatar(int id, [FromForm] FormImage userFile) =>
            Ok(await imageService.UploadImage(id, userFile.Image));

        /// <summary>
        /// Deletes the user avatar.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpDelete("{id}/avatar")]
        public async Task<IActionResult> DeleteUserAvatar(int id) => Ok(await imageService.DeleteImage(id));

        /// <summary>
        /// Gets the user file by identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>Base64 array of user avatar</returns>
        [HttpGet("{id}/avatar")]
        public async Task<IActionResult> GetUserFileById(int id) =>
            Ok(await imageService.GetImageBytesById(id));
    }
}
