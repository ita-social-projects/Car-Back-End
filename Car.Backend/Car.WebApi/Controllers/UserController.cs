using System.Threading.Tasks;
using Car.Domain.Models;
using Car.Domain.Models.User;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
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
        /// <param name="updateUserModel">User object to update.</param>
        /// <returns>Updated user.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromForm] UpdateUserModel updateUserModel) =>
            Ok(await userService.UpdateUserAsync(updateUserModel));
    }
}
