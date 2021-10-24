using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
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
        public async Task<IActionResult> MarkMessagesRead(int chatId)
        {
            var isCountUpdated = await receivedMessagesService.MarkMessagesReadInChatAsync(chatId);
            return isCountUpdated ? Ok() : Forbid();
        }

        /// <summary>
        /// Counts number of all unread messages of the user
        /// </summary>
        /// <returns>Number of all unread messages</returns>
        [HttpGet("unreadNumber")]
        public async Task<IActionResult> GetAllUnreadMessagesNumber() =>
            Ok(await receivedMessagesService.GetAllUnreadMessagesNumber());
    }
}
