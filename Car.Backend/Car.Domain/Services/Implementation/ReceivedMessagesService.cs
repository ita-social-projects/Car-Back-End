using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IRepository<User> userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IRepository<Journey> journeyRepository;

        public ReceivedMessagesService(
            IRepository<ReceivedMessages> receivedMessagesRepository,
            IRepository<User> userRepository,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper,
            IRepository<Journey> journeyRepository)
        {
            this.receivedMessagesRepository = receivedMessagesRepository;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
            this.journeyRepository = journeyRepository;
        }

        public async Task AddReceivedMessages(Chat chat)
        {
            var addedReceivedMessages = await GetReceivedMessages(chat.Id);

            if (addedReceivedMessages is not null)
            {
                await receivedMessagesRepository.AddAsync(mapper.Map<ReceivedMessages>(addedReceivedMessages));
                await receivedMessagesRepository.SaveChangesAsync();
            }
        }

        public async Task<ReceivedMessages> GetReceivedMessages(int chatId)
        {
            var journey = await journeyRepository.Query()
                .FirstOrDefaultAsync(j => j.ChatId == chatId);
            var addedReceivedMessages = new ReceivedMessages()
            {
                ChatId = chatId,
                UserId = journey.OrganizerId,
                UnreadMessagesCount = default(int),
            };
            return addedReceivedMessages;
        }

        public async Task<bool> MarkMessagesReadInChatAsync(int chatId)
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            var receivedMessages = await receivedMessagesRepository.Query()
                .FirstOrDefaultAsync(rm => rm.ChatId == chatId
                                           && rm.UserId == userId);

            if (receivedMessages is null)
            {
                return false;
            }

            receivedMessages.UnreadMessagesCount = 0;
            await receivedMessagesRepository.SaveChangesAsync();

            return true;
        }

        public async Task<int> GetAllUnreadMessagesNumber()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            return await receivedMessagesRepository
                .Query()
                .Where(repo => repo.UserId == userId)
                .SumAsync(repo => repo.UnreadMessagesCount);
        }
    }
}
