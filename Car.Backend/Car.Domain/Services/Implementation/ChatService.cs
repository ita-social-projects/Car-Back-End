using System;
using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ChatService : IChatService
    {
        private readonly IUnitOfWork<User> unitOfWorkUser;
        private readonly IUnitOfWork<Chat> unitOfWorkChat;
        private readonly IUnitOfWork<Message> unitOfWorkMessage;

        public ChatService(IUnitOfWork<User> unitOfWorkUser, IUnitOfWork<Chat> unitOfWorkChat, IUnitOfWork<Message> unitOfWorkMessage)
        {
            this.unitOfWorkUser = unitOfWorkUser;
            this.unitOfWorkChat = unitOfWorkChat;
            this.unitOfWorkMessage = unitOfWorkMessage;
        }

        public Chat GetChatById(int chatId)
        {
            var chat = unitOfWorkChat.GetRepository().Query(message => message.Messages).FirstOrDefault(p => p.Id == chatId);
            chat.Messages = chat.Messages.OrderByDescending(time => time.CreatedAt).ToList();
            return chat;
        }

        public Chat AddChat(Chat chat)
        {
            var addedChat = unitOfWorkChat.GetRepository().Add(chat);
            unitOfWorkUser.SaveChanges();
            return addedChat;
        }

        public User AddUserToChat(int userId, int chatId)
        {
            var user = unitOfWorkUser.GetRepository().GetById(userId);
            var chat = unitOfWorkChat.GetRepository().Query().FirstOrDefault(chat => chat.Id == chatId);
            unitOfWorkChat.SaveChanges();
            return user;
        }

        public List<Chat> GetUsersChats(int userId)
        {
            var user = unitOfWorkUser.GetRepository().Query()
                .Include(organizer => organizer.OrganizerJourneys)
                .ThenInclude(chat => chat.Chat)
                .Include(participant => participant.ParticipantJourneys)
                .ThenInclude(chat => chat.Chat)
                .Include(participant => participant.ParticipantJourneys)
                .ThenInclude(o => o.Organizer)
                .FirstOrDefault(user => user.Id == userId);

            if (user == null)
            {
                return new List<Chat>();
            }

            var chats = user.OrganizerJourneys.Select(journey => journey.Chat).ToList();
            chats.AddRange(user.ParticipantJourneys.Select(journey => journey.Chat));

            return chats;
        }

        public Message AddMessage(Message message)
        {
            var addedMessage = unitOfWorkMessage.GetRepository().Add(message);
            unitOfWorkChat.SaveChanges();
            return addedMessage;
        }
    }
}