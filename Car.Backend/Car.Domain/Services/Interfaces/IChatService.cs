using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Dto.Chat;
using Car.Domain.Filters;

namespace Car.Domain.Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<ChatDto>> GetUserChatsAsync();

        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId, int previousMessageId);

        Task<Chat?> AddChatAsync(int baseJourneyId);

        Task<Message> AddMessageAsync(Message message);

        Task IncrementUnreadMessagesAsync(int chatId, int senderId);

        Task<IEnumerable<ChatDto>> GetFilteredChatsAsync(ChatFilter filter);

        Task<Chat> GetChatByIdAsync(int chatId);

        Task DeleteUnnecessaryChatAsync();

        Task UnsubscribeUserFromChat(int userId, int chatId);
    }
}
