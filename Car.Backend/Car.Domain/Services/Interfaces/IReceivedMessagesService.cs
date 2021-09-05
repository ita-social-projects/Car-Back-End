using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.Domain.Services.Interfaces
{
    public interface IReceivedMessagesService
    {
        Task<int> MarkMessagesReadInChatAsync(int userId, int chatId);

        Task<int> GetUnreadMessageForChatAsync(int userId, int chatId);
    }
}
