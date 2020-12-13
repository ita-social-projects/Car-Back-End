using System.Threading.Tasks;
using Car.BLL.Dto;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using File = Google.Apis.Drive.v3.Data.File;

namespace CarBackEnd.Controllers
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
        public IActionResult GetUserById(int userId)
        {
            return Ok(userService.GetUserById(userId));
        }

        /// <summary>
        /// Uploads the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="userFile">The user file.</param>
        /// <returns>The user entity</returns>
        [HttpPut("{userId}/avatar")]
        public async Task<IActionResult> UploadUserAvatar(int userId, [FromForm] FormImage userFile)
        {
           return Ok(await imageService.UploadImage(userId, userFile.image));
        }

        /// <summary>
        /// Deletes the user avatar.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpDelete("{userId}/avatar")]
        public async Task<IActionResult> DeleteUserAvatar(int userId)
        {
            return Ok(await imageService.DeleteImage(userId));
        }

        /// <summary>
        /// Gets the user file by identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>Base64 array of user avatar</returns>
        [HttpGet("avatar/{userId}")]
        public async Task<IActionResult> GetUserFileById(int userId)
        {
            return Ok(await imageService.GetImageBytesById(userId));
        }
    }
}
