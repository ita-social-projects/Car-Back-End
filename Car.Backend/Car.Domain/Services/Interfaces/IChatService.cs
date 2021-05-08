using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Models.Chat;

namespace Car.Domain.Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<ChatModel>> GetUserChatsAsync(int userId);

        Task<IEnumerable<MessageDto>> GetMessagesByChatIdAsync(int chatId, int previousMessageId);

        Task<Chat> AddChatAsync(Chat chat);

        Task<Message> AddMessageAsync(Message message);
    }
}
