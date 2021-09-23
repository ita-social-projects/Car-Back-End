using System.Threading.Tasks;
using Car.Domain.Dto.Chat;
using Car.Domain.Filters;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/user-chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IChatService chatService;

        public ChatController(IChatService chatService) =>
            this.chatService = chatService;

        /// <summary>
        /// Get the current user chats
        /// </summary>
        /// <returns>Chats of Sender by Id</returns>
        [HttpGet]
        public async Task<IActionResult> GetUserChats() =>
            Ok(await chatService.GetUserChatsAsync());

        /// <summary>
        /// Get chat by Chat Id
        /// </summary>
        /// <param name="id">Chat identifier</param>
        /// <param name="previousMessageId">Oldest message identifier</param>
        /// <returns>Chat</returns>
        [HttpGet("chat/{id}/{previousMessageId}")]
        public async Task<IActionResult> GetChat(int id, int previousMessageId) =>
            Ok(await chatService.GetMessagesByChatIdAsync(id, previousMessageId));

        /// <summary>
        /// Add the chat
        /// </summary>
        /// <param name="chat">Sender identifier</param>
        /// <returns>New chat</returns>
        [HttpPost]
        public async Task<IActionResult> AddChat([FromBody] CreateChatDto chat)
        {
            var result = await chatService.GetChatByIdAsync(chat.Id);
            result ??= await chatService.AddChatAsync(chat);

            return Ok(result);
        }

        /// <summary>
        /// Filters chats by conditions
        /// </summary>
        /// <param name="filter">Chat filter parameters</param>
        /// <returns>Messages</returns>
        [HttpPost("filter/")]
        public async Task<IActionResult> GetFiltered([FromBody] ChatFilter filter) =>
            Ok(await chatService.GetFilteredChatsAsync(filter));
    }
}