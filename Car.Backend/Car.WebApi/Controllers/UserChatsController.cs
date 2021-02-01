using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/user-chats")]
    [ApiController]
    public class UserChatsController : ControllerBase
    {
        private readonly IUserChatsManager userManager;

        public UserChatsController(IUserChatsManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the user chats by User Id
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>UserChats of User by Id</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserChats(int id)
        {
            return Ok(userManager.GetUsersChats(id));
        }
    }
}
