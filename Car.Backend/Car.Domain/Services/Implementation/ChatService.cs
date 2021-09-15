using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.Chat;
using Car.Domain.Extensions;
using Car.Domain.Filters;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ReceivedMessages> receivedMessagesRepository;
        private readonly IRepository<Journey> journeyRepository;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ChatService(
            IRepository<User> userRepository,
            IRepository<Chat> chatRepository,
            IRepository<Message> messageRepository,
            IRepository<ReceivedMessages> receivedMessagesRepository,
            IRepository<Journey> journeyRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
            this.receivedMessagesRepository = receivedMessagesRepository;
            this.journeyRepository = journeyRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId, int previousMessageId)
        {
            var messages = messageRepository
                .Query()
                .Where(message => message.ChatId == chatId);
            var maxId = messages.Any()
                ? messages.Max(message => message.Id) + 1
                : default;
            var chat = await messageRepository
                .Query()
                .Include(user => user.Sender)
                .Where(message => message.ChatId == chatId
                                  && message.Id < (previousMessageId == 0
                                      ? maxId
                                      : previousMessageId))
                .Select(message => new MessageDto()
                {
                    Id = message.Id,
                    ChatId = message.ChatId,
                    CreatedAt = message.CreatedAt,
                    Text = message.Text,
                    SenderId = message.Sender!.Id,
                    SenderName = message.Sender.Name,
                    SenderSurname = message.Sender.Surname,
                    ImageId = message.Sender.ImageId,
                })
                .OrderByDescending(messageDto => messageDto.CreatedAt)
                .Take(50)
                .ToListAsync();

            return chat;
        }

        public async Task<Chat?> AddChatAsync(CreateChatDto chat)
        {
            var addedChat = await chatRepository.AddAsync(mapper.Map<Chat>(chat));
            if (addedChat is not null)
            {
                var addedReceivedMessages = await GetReceivedMessagesFromChat(addedChat.Id);
                await receivedMessagesRepository.AddAsync(mapper.Map<ReceivedMessages>(addedReceivedMessages));
                await receivedMessagesRepository.SaveChangesAsync();
            }

            await chatRepository.SaveChangesAsync();

            return addedChat;
        }

        public async Task<ReceivedMessages> GetReceivedMessagesFromChat(int chatId)
        {
            var userId = await journeyRepository.Query()
                .FirstOrDefaultAsync(j => j.Id == chatId);
            var addedReceivedMessages = new ReceivedMessages()
            {
                ChatId = chatId,
                UserId = userId.OrganizerId,
                UnreadMessagesCount = default(int),
            };
            return addedReceivedMessages;
        }

        public async Task<IEnumerable<ChatDto>> GetUserChatsAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            var user = await userRepository.Query()
                .IncludeJourney()
                .IncludeReceivedMessages()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var chats = user.ReceivedMessages.Select(rm => rm.Chat)
                .Except(new List<Chat>() { null! })
                .OrderByDescending(chat => chat?.Journey?.DepartureTime);

            var res = mapper.Map<IEnumerable<Chat>, IEnumerable<ChatDto>>(chats!);
            return res;
        }

        public async Task<IEnumerable<ChatDto>> GetFilteredChatsAsync(ChatFilter filter)
        {
            var messages = await messageRepository.Query()
                .Where(msg => filter.Chats!
                    .Select(chat => chat.Id)
                    .Contains(msg.ChatId))
                .Where(msg => msg.Text
                    .Contains(filter.SearchText))
                .OrderByDescending(message => message.CreatedAt)
                .ToListAsync();

            var result = messages
                .SelectMany(msg => filter.Chats!
                    .Where(chat => msg.ChatId == chat.Id)
                    .Select(chat => new ChatDto()
                    {
                        Id = chat.Id,
                        Journey = chat.Journey,
                        JourneyOrganizer = chat.JourneyOrganizer,
                        MessageText = msg.Text,
                        MessageId = msg.Id,
                        Name = chat.Name,
                        ReceivedMessages = chat.ReceivedMessages,
                    }))
                .ToList();

            return result;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            var addedMessage = await messageRepository.AddAsync(message);
            await IncrementUnreadMessagesAsync(message.ChatId, message.SenderId);
            await messageRepository.SaveChangesAsync();
            return addedMessage;
        }

        public async Task IncrementUnreadMessagesAsync(int chatId, int senderId)
        {
            var receivedMessages = receivedMessagesRepository
                .Query()
                .Where(rm => rm.ChatId == chatId && rm.UserId != senderId)
                .ToList();

            foreach (var receivedMessage in receivedMessages)
            {
                receivedMessage.UnreadMessagesCount++;
            }

            await chatRepository.SaveChangesAsync();
        }

        public async Task<int> GetAllUnreadMessagesNumber()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId();
            return await receivedMessagesRepository
                .Query()
                .Where(repo => repo.UserId == userId)
                .SumAsync(repo => repo.UnreadMessagesCount);
        }

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await chatRepository.GetByIdAsync(chatId);
        }
    }
}