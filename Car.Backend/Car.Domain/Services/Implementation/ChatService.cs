using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
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

        public Task<Chat> GetChatByIdAsync(int chatId)
        {
            return chatRepository
                .Query(message => message.Messages.OrderByDescending(m => m.CreatedAt))
                .FirstOrDefaultAsync(p => p.Id == chatId);
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
                .Union(user.ParticipantJourneys.Select(journey => journey.Chat));

            return chats;
        }

        public async Task<Message> AddMessageAsync(Message message)
        {
            var addedMessage = await messageRepository.AddAsync(message);
            await chatRepository.SaveChangesAsync();

            return addedMessage;
        }
    }
}