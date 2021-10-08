using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ReceivedMessagesService : IReceivedMessagesService
    {
        private readonly IRepository<ReceivedMessages> receivedMessagesRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ReceivedMessagesService(
            IRepository<ReceivedMessages> receivedMessagesRepository,
            IHttpContextAccessor httpContextAccessor)
        {
            this.receivedMessagesRepository = receivedMessagesRepository;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> MarkMessagesReadInChatAsync(int chatId)
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();

            var receivedMessages = await receivedMessagesRepository.Query()
                .FirstOrDefaultAsync(rm => rm.ChatId == chatId);

            if (receivedMessages.UserId != userId)
            {
                return false;
            }

            if (receivedMessages != null)
            {
                receivedMessages.UnreadMessagesCount = 0;
                await receivedMessagesRepository.SaveChangesAsync();
            }

            return true;
        }

        public async Task<int> GetAllUnreadMessagesNumber()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            return await receivedMessagesRepository
                .Query()
                .Where(repo => repo.UserId == userId)
                .SumAsync(repo => repo.UnreadMessagesCount);
        }
    }
}
