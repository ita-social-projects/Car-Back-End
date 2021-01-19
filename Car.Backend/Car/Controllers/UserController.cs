using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Controllers
{
    [Route("api/[controller]")]
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
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpGet("{userId}")]
        public IActionResult GetUserById(int userId) => Ok(userService.GetUserById(userId));

        /// <summary>
        /// Gets the user with avatar by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user dto with avatar</returns>
        [HttpGet("withAvatar/{userId}")]
        public IActionResult GetUserWithAvatarById(int userId) => Ok(userService.GetUserWithAvatarById(userId));

        /// <summary>
        /// Uploads the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userFile">The user file.</param>
        /// <returns>The user entity</returns>
        [HttpPut("{userId}/avatar")]
        public async Task<IActionResult> UploadUserAvatar(int userId, [FromForm] FormImage userFile) =>
            Ok(await imageService.UploadImage(userId, userFile.Image));

        /// <summary>
        /// Deletes the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpDelete("{userId}/avatar")]
        public async Task<IActionResult> DeleteUserAvatar(int userId) => Ok(await imageService.DeleteImage(userId));

        /// <summary>
        /// Gets the user file by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Base64 array of user avatar</returns>
        [HttpGet("{userId}/avatar")]
        public async Task<IActionResult> GetUserFileById(int userId) =>
            Ok(await imageService.GetImageBytesById(userId));
    }
}
