using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class UserChatsManager : IUserChatsManager
    {
        private readonly IUnitOfWork<UserChat> unitOfWork;

        public UserChatsManager(IUnitOfWork<UserChat> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public List<Chat> GetUsersChats(int userId)
        {
            return unitOfWork.GetRepository()
                .Query(chat => chat.Chat)
                .Where(user => user.UserId == userId)
                .Select(chat => chat.Chat).ToList();
        }
    }
}
