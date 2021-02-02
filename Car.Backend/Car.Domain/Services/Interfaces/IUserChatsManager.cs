using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserChatsManager
    {
        public List<Chat> GetUsersChats(int userId);

        public Chat GetChatById(int chatId);

        public Chat AddChat(Chat chat);

        public User AddUserToChat(int chatId, int userId);
    }
}
