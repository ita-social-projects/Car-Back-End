using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Extensions;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<Chat> chatRepository;
        private readonly IRepository<Message> messageRepository;

        public ChatService(IRepository<User> userRepository, IRepository<Chat> chatRepository, IRepository<Message> messageRepository)
        {
            this.userRepository = userRepository;
            this.chatRepository = chatRepository;
            this.messageRepository = messageRepository;
        }

        public async Task<IEnumerable<MessageDto>> GetChatByIdAsync(int chatId, int previousMessageId)
        {
            var chat = await messageRepository.Query()
                .Include(u => u.Sender)
                .Where(c => c.ChatId == chatId)
                .Select(q => new MessageDto()
                {
                    Id = q.Id,
                    ChatId = q.ChatId,
                    CreatedAt = q.CreatedAt,
                    Text = q.Text,
                    SenderId = q.Sender.Id,
                    SenderName = q.Sender.Name,
                    SenderSurname = q.Sender.Surname,
                    ImageId = q.Sender.ImageId,
                }).OrderByDescending(d => d.CreatedAt)
                .Where(q => q.Id < (previousMessageId == 0 ? messageRepository
                    .Query()
                    .Where(m => m.ChatId == chatId)
                    .Max(q => q.Id) + 1 : previousMessageId))
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

        public async Task<IEnumerable<Chat>> GetUserChatsAsync(int userId)
        {
            var user = await userRepository.Query()
                .IncludeChats()
                .FirstOrDefaultAsync(u => u.Id == userId);

            var chats = user?.OrganizerJourneys.Select(journey => journey.Chat)
                .Union(user.ParticipantJourneys.Select(journey => journey.Chat))
                .Except(new List<Chat>() { null });

            return chats;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            var addedMessage = await messageRepository.AddAsync(message);
            await messageRepository.SaveChangesAsync();
            return addedMessage;
        }
    }
}