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

        [HttpPut("{userId}/{chatId}")]
        public async Task<IActionResult> MarkMessagesRead(int userId, int chatId) =>
            Ok(await receivedMessagesService.MarkMessagesReadInChatAsync(userId, chatId));
    }
}
