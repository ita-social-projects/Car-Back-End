using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/user-chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        /// <summary>
        /// Get the user chats by Sender Id
        /// </summary>
        /// <param name="id">Sender identifier</param>
        /// <returns>Chats of Sender by Id</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserChats(int id) =>
            Ok(await chatService.GetUserChatsAsync(id));

        /// <summary>
        /// Get chat by Chat Id
        /// </summary>
        /// <param name="id">Sender identifier</param>
        /// <returns>Chat</returns>
        [HttpGet("chat/{id}")]
        public async Task<IActionResult> GetChat(int id) =>
            Ok(await chatService.GetChatByIdAsync(id));

        /// <summary>
        /// Add the chat
        /// </summary>
        /// <param name="chat">Sender identifier</param>
        /// <returns>New chat</returns>
        [HttpPost]
        public async Task<IActionResult> AddChat([FromBody] Chat chat) =>
            Ok(await chatService.AddChatAsync(chat));

        /// <summary>
        /// Add new message
        /// </summary>
        /// <param name="message">Message entity</param>
        /// <returns>Added Message</returns>
        [HttpPost("message")]
        public async Task<IActionResult> AddMessage([FromBody] Message message) =>
            Ok(await chatService.AddMessageAsync(message));
    }
}