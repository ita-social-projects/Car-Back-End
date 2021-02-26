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

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns>The user entity</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) => Ok(userService.GetUserById(id));
    }
}
