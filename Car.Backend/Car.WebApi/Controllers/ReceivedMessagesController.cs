using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/receivedmessages")]
    [ApiController]
    public class ReceivedMessagesController : ControllerBase
    {
        private readonly IReceivedMessagesService receivedMessagesService;

        public ReceivedMessagesController(IReceivedMessagesService receivedMessagesService)
        {
            this.receivedMessagesService = receivedMessagesService;
        }

        /// <summary>
        /// Mark specified chat messages as read
        /// </summary>
        /// <param name="chatId">Chat identifier</param>
        /// <returns>number of marked messages</returns>
        [HttpPut("markasread/{chatId}")]
        public async Task<IActionResult> MarkMessagesRead(int chatId) =>
            Ok(await receivedMessagesService.MarkMessagesReadInChatAsync(chatId));

        /// <summary>
        /// Counts number of all unread messages of the user
        /// </summary>
        /// <returns>Number of all unread messages</returns>
        [HttpGet("unreadNumber")]
        public async Task<IActionResult> GetAllUnreadMessagesNumber() =>
            Ok(await receivedMessagesService.GetAllUnreadMessagesNumber());

        /// <summary>
        /// Get number of unread messages from the specified chat
        /// </summary>
        /// <param name="chatId">Chat identifier</param>
        /// <returns>number of unread messages</returns>
        [HttpGet("getunreadmessages/{chatId}")]
        public async Task<IActionResult> GetUnreadMessageForChat(int chatId) =>
            Ok(await receivedMessagesService.GetUnreadMessageForChatAsync(chatId));
    }
}
