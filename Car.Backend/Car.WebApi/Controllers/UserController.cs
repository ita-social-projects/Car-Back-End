using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) =>
            this.userService = userService;

        /// <summary>
        /// Gets the user by identifier asynchronously.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id) =>
            Ok(await userService.GetUserByIdAsync(id));

        /// <summary>
        /// Updates a users image with the identifier asynchronously.
        /// </summary>
        /// <param name="updateUserImageDto">User object to update.</param>
        /// <returns>Updated user.</returns>
        [HttpPut("image")]
        public async Task<IActionResult> UpdateUserImage([FromForm] UpdateUserImageDto updateUserImageDto) =>
            Ok(await userService.UpdateUserImageAsync(updateUserImageDto));

        /// <summary>
        /// Updates a users fcmtoken with the identifier asynchronously.
        /// </summary>
        /// <param name="updateUserFcmtokenDto">User object to update.</param>
        /// <returns>Updated user.</returns>
        [HttpPut("fcmtoken")]
        public async Task<IActionResult> UpdateUserFcmtoken([FromForm] UpdateUserFcmtokenDto updateUserFcmtokenDto) =>
            Ok(await userService.UpdateUserFcmtokenAsync(updateUserFcmtokenDto));
    }
}
