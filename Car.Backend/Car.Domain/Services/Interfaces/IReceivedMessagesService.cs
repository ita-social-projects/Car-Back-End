using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IReceivedMessagesService
    {
        Task<int> MarkMessagesReadInChatAsync(int chatId);

        Task<int> GetAllUnreadMessagesNumber();
    }
}
