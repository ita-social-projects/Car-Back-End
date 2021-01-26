using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/[controller]")]
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
        /// <param name="userId">User indetifier</param>
        /// <returns>Chats of User by Id</returns>
        [HttpGet("{userId}")]
        public IActionResult GetUserChats(int userId)
        {
            return Ok(userManager.GetUsersChats(userId));
        }
    }
}
