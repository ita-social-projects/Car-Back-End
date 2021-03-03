using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IChatService
    {
        Task<IEnumerable<Chat>> GetUserChatsAsync(int userId);

        Task<Chat> GetChatByIdAsync(int chatId);

        Task<Chat> AddChatAsync(Chat chat);

        Task<Message> AddMessageAsync(Message message);
    }
}
