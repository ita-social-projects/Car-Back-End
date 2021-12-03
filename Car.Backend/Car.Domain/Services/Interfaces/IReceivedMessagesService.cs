using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IReceivedMessagesService
    {
        Task AddReceivedMessages(Chat chat);

        Task<ReceivedMessages> GetReceivedMessages(int chatId);

        Task<bool> MarkMessagesReadInChatAsync(int chatId);

        Task<int> GetAllUnreadMessagesNumber();
    }
}
