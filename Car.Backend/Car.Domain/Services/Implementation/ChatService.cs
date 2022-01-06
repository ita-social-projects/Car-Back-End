using System;
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
        private readonly IReceivedMessagesService receivedMessagesService;

        public ChatService(
            IRepository<User> userRepository,
            IRepository<Chat> chatRepository,
            IRepository<Message> messageRepository,
            IRepository<ReceivedMessages> receivedMessagesRepository,
            IRepository<Journey> journeyRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IReceivedMessagesService receivedMessagesService)
        {
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
            this.receivedMessagesRepository = receivedMessagesRepository;
            this.journeyRepository = journeyRepository;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.receivedMessagesService = receivedMessagesService;
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

        public async Task<Chat?> AddChatAsync(int baseJourneyId)
        {
            var addedChat = chatRepository.Query().Include(x => x.Journeys).FirstOrDefault(c => c.Journeys.Any(j => j.Id == baseJourneyId));

            if (addedChat is null)
            {
                var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
                var user = await userRepository
                    .Query()
                    .IncludeChatsAndMessages()
                    .IncludeReceivedMessages()
                    .FirstOrDefaultAsync(u => u.Id == userId);

                addedChat = await chatRepository.AddAsync(mapper.Map<Chat>(new CreateChatDto()
                {
                    Name = $"{user.Name} {user.Surname}'s ride",
                }));

                await chatRepository.SaveChangesAsync();

                if (addedChat is not null)
                {
                    await journeyRepository
                        .Query()
                        .Where(j => j.ParentId == baseJourneyId || j.Id == baseJourneyId)
                        .ForEachAsync(j => j.ChatId = addedChat.Id);

                    await journeyRepository.SaveChangesAsync();
                    await receivedMessagesService.AddReceivedMessages(addedChat);
                }
            }

            return addedChat;
        }

        public async Task<IEnumerable<ChatDto>> GetUserChatsAsync()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var user = await userRepository
                .Query()
                .IncludeChatsAndMessages()
                .IncludeReceivedMessages()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var chats = user.OrganizerJourneys.Select(oj => oj.Chat)
                .Union(user.ParticipantJourneys.Select(pj => pj.Chat))
                .OrderByDescending(chat => chat!.Messages!
                    .Any() ? chat.Messages
                    .OrderByDescending(m => m.CreatedAt)
                    .First().CreatedAt : (DateTime?)null)
                .ThenBy(chat => chat!.Journeys!.FirstOrDefault()!.DepartureTime);

            return mapper.Map<IEnumerable<Chat>, IEnumerable<ChatDto>>(chats!);
        }

        public async Task<IEnumerable<ChatDto>> GetFilteredChatsAsync(ChatFilter filter)
        {
            var messages = await messageRepository.Query()
                .Where(msg => filter.Chats!
                    .Select(chat => chat.Id)
                    .Contains(msg.ChatId)).ToListAsync();
            messages = messages.Where(msg => msg.Text.Split(' ', StringSplitOptions.None)
                .Any(wrd => wrd.StartsWith(filter.SearchText)))
                .OrderByDescending(message => message.CreatedAt).ToList();
            var chats = filter!.Chats!.Where(chat => chat.Name.StartsWith(filter.SearchText)).ToList();
            var result = messages.SelectMany(msg => filter.Chats!
                    .Where(chat => msg.ChatId == chat.Id)
                    .Select(chat => new ChatDto()
                    {
                        Id = chat.Id,
                        Journeys = chat.Journeys,
                        JourneyOrganizer = chat.JourneyOrganizer,
                        MessageText = msg.Text,
                        MessageId = msg.Id,
                        Name = chat.Name,
                        ReceivedMessages = chat.ReceivedMessages,
                    }))
                .Union(chats);
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

        public async Task<Chat> GetChatByIdAsync(int chatId)
        {
            return await chatRepository.GetByIdAsync(chatId);
        }

        public async Task DeleteUnnecessaryChatAsync()
        {
            var chatsToDelete = chatRepository
                .Query()
                .Include(chat => chat.ReceivedMessages)
                .Include(chat => chat.Journeys)
                .AsEnumerable()
                .Where(chat => chat.Journeys == null || !chat.Journeys.Any());

            if (chatsToDelete.Any())
            {
                await chatRepository.DeleteRangeAsync(chatsToDelete);
                await chatRepository.SaveChangesAsync();
            }
        }
    }
}
