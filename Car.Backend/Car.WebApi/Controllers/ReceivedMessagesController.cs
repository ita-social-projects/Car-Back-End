using System.Threading.Tasks;
using Car.Domain.Services;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        [HttpPut("markasread/{chatId}/{userId}")]
        public async Task<IActionResult> MarkMessagesRead(int userId, int chatId) =>
            Ok(await receivedMessagesService.MarkMessagesReadInChatAsync(userId, chatId));

        [HttpGet("getunreadmessages/{chatId}/{userId}")]
        public async Task<IActionResult> GetUnreadMessageForChat(int userId, int chatId) =>
            Ok(await receivedMessagesService.GetUnreadMessageForChatAsync(userId, chatId));

        /// <summary>
        /// Counts number of all unread messages of the user
        /// </summary>
        /// <returns>Number of all unread messages</returns>
        [HttpGet("unreadNumber")]
        public async Task<IActionResult> GetAllUnreadMessagesNumber() =>
            Ok(await receivedMessagesService.GetAllUnreadMessagesNumber());
    }
}
