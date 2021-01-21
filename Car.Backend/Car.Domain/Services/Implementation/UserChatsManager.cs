using Car.Domain.Services.Interfaces;
using Car.Data.Entities;
using Car.Data.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Car.Domain.Services.Implementation
{
    public class UserChatsManager : IUserChatsManager
    {
        private readonly IUnitOfWork<UserChat> unitOfWork;

        public UserChatsManager(IUnitOfWork<UserChat> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Chat> GetUserChats(int userId)
        {
            return unitOfWork.GetRepository()
                .Query(chat => chat.Chat)
                .Where(user => user.UserId == userId)
                .Select(chat => chat.Chat).ToList();
        }
    }
}
