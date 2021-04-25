using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetUserChatsAsync(int userId);

        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId, int previousMessageId);

        Task<Chat> AddChatAsync(Chat chat);

        Task<Message> AddMessageAsync(Message message);

        Task DeleteOutdatedChatsAsync();
    }
}
