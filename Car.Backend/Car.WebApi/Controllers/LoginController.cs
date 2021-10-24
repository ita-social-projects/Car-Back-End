using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
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
        /// <returns>User for a client app</returns>
        [HttpPost]
        public async Task<IActionResult> Login() =>
            Ok(await loginService.LoginAsync());
    }
}
