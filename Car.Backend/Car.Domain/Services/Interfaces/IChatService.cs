using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IChatService
    {
        public List<Chat> GetUsersChats(int userId);

        public Chat GetChatById(int chatId);

        public Chat AddChat(Chat chat);

        public User AddUserToChat(int userId, int chatId);
    }
}
