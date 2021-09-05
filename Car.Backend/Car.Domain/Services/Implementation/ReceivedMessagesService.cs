using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ReceivedMessagesService : IReceivedMessagesService
    {
        private readonly IRepository<ReceivedMessages> receivedMessagesRepository;

        public ReceivedMessagesService(
            IRepository<ReceivedMessages> receivedMessagesRepository)
        {
            this.receivedMessagesRepository = receivedMessagesRepository;
        }

        public async Task<int> MarkMessagesReadInChatAsync(int userId, int chatId)
        {
            var receivedMessages = await receivedMessagesRepository.Query()
                .FirstOrDefaultAsync(rm => rm.ChatId == chatId && rm.UserId == userId)!;
            receivedMessages.UnreadMessagesCount = 0;

            await receivedMessagesRepository.SaveChangesAsync();
            return receivedMessages.UnreadMessagesCount;
        }

        public async Task<int> GetUnreadMessageForChatAsync(int userId, int chatId)
        {
            var receivedMessages = await receivedMessagesRepository.Query()
                .FirstOrDefaultAsync(rm => rm.ChatId == chatId && rm.UserId == userId)!;

            return receivedMessages.UnreadMessagesCount;
        }
    }
}
