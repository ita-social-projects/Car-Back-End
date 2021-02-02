using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;
using Google.Apis.Util;

namespace Car.Domain.Services.Implementation
{
    public class ChatService : IUserChatsManager
    {
        private readonly IUnitOfWork<User> unitOfWorkUser;
        private readonly IUnitOfWork<Chat> unitOfWorkChat;

        public ChatService(IUnitOfWork<User> unitOfWorkUser, IUnitOfWork<Chat> unitOfWorkChat)
        {
            this.unitOfWorkUser = unitOfWorkUser;
            this.unitOfWorkChat = unitOfWorkChat;
        }

        public Chat GetChatById(int chatId)
        {
            return unitOfWorkChat.GetRepository().Query(u => u.Users).FirstOrDefault(p => p.Id == chatId);
        }

        public Chat AddChat(Chat chat)
        {
            var newChat = new Chat
            {
                Id = chat.Id,
                ChatName = chat.ChatName,
                Users = new List<User>(),
            };

            var addedChat = unitOfWorkChat.GetRepository().Add(newChat);
            unitOfWorkUser.SaveChanges();
            return addedChat;
        }

        public User AddUserToChat(int userId, int chatId)
        {
            var user = unitOfWorkUser.GetRepository().GetById(userId);
            var chat = unitOfWorkChat.GetRepository().Query(u => u.Users).FirstOrDefault(c => c.Id == chatId);
            chat?.Users.Add(user);
            unitOfWorkChat.SaveChanges();
            return user;
        }

        public List<Chat> GetUsersChats(int userId)
        {
            var user = unitOfWorkUser.GetRepository().Query(u => u.Chats).FirstOrDefault(i => i.Id == userId);
            return user?.Chats.ToList();
        }
    }
}
