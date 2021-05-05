using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Models.Chat;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IMapper mapper;

        public ChatService(
            IRepository<User> userRepository,
            IRepository<Chat> chatRepository,
            IRepository<Message> messageRepository,
            IMapper mapper)
        {
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId, int previousMessageId)
        {
            var chat = await messageRepository.Query()
                .Include(user => user.Sender)
                .Where(message => message.ChatId == chatId)
                .Select(message => new MessageDto()
                {
                    Id = message.Id,
                    ChatId = message.ChatId,
                    CreatedAt = message.CreatedAt,
                    Text = message.Text,
                    SenderId = message.Sender.Id,
                    SenderName = message.Sender.Name,
                    SenderSurname = message.Sender.Surname,
                    ImageId = message.Sender.ImageId,
                }).OrderByDescending(messageDto => messageDto.CreatedAt)
                .Where(messageDto => messageDto.Id < (previousMessageId == 0 ? messageRepository
                    .Query()
                    .Where(message => message.ChatId == chatId)
                    .Max(message => message.Id) + 1 : previousMessageId))
                .Take(50)
                .ToListAsync();
            return chat;
        }

        public async Task<Chat> AddChatAsync(Chat chat)
        {
            var addedChat = await chatRepository.AddAsync(chat);
            await userRepository.SaveChangesAsync();

            return addedChat;
        }

        public async Task<IEnumerable<ChatModel>> GetUserChatsAsync(int userId)
        {
            var user = await userRepository.Query()
                .IncludeChats()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var chats = user?.OrganizerJourneys.Select(journey => journey.Chat)
                .Union(user.ParticipantJourneys.Select(journey => journey.Chat))
                .Except(new List<Chat>() { null });

            var result = mapper.Map<IEnumerable<Chat>, IEnumerable<ChatModel>>(chats);
            return result.Count() == 0 ? null : result;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            var addedMessage = await messageRepository.AddAsync(message);
            await messageRepository.SaveChangesAsync();
            return addedMessage;
        }
    }
}
