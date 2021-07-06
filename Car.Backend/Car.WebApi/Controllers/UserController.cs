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
        /// Updates a user with the identifier asynchronously.
        /// </summary>
        /// <param name="updateUserDto">User object to update.</param>
        /// <returns>Updated user.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserDto updateUserDto) =>
            Ok(await userService.UpdateUserAsync(updateUserDto));
    }
}
