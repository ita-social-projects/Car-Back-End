using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService loginService;

        public LoginController(ILoginService loginService) =>
            this.loginService = loginService;

        /// <summary>
        /// ensures the user and returns a User for client app,
        /// if user doesn't exist in DB it creates a user and saves them to DB
        /// </summary>
        /// <param name="user">Sender model params</param>
        /// <returns>User for a client app</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserDto user) =>
            Ok(await loginService.LoginAsync(user));
    }
}
