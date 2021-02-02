﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/user-chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IUserChatsManager userManager;

        public ChatController(IUserChatsManager userManager)
        {
            this.userManager = userManager;
        }

        /// <summary>
        /// Get the user chats by User Id
        /// </summary>
        /// <param name="id">User indetifier</param>
        /// <returns>Chats of User by Id</returns>
        [HttpGet("{id}")]
        public IActionResult GetUserChats(int id)
        {
            return Ok(userManager.GetUsersChats(id));
        }

        /// <summary>
        /// Get chat by Chat Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Chat</returns>
        [HttpGet("chat/{id}")]
        public IActionResult GetChat(int id)
        {
            return Ok(userManager.GetChatById(id));
        }

        /// <summary>
        /// Add the chat
        /// </summary>
        /// <param name="chat"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddChat([FromBody] Chat chat)
        {
            return Ok(userManager.AddChat(chat));
        }

        /// <summary>
        /// Add user to the chat
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="chatId"></param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult AddUserToChat(int userId, int chatId)
        {
            return Ok(userManager.AddUserToChat(chatId, userId));
        }
    }
}
