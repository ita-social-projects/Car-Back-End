﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChatsController : ControllerBase
    {
        private readonly IUserChatsManager userChatsService;

        public UserChatsController(IUserChatsManager userChatsService)
        {
            this.userChatsService = userChatsService;
        }

        /// <summary>
        /// Get the user chats by User Id
        /// </summary>
        /// <param name="userId">User indetifier</param>
        /// <returns>Chats of User by Id</returns>
        [HttpGet("{userId}")]
        public IActionResult GetUserChats(int userId)
        {
            return Ok(userChatsService.GetUserChats(userId));
        }
    }
}