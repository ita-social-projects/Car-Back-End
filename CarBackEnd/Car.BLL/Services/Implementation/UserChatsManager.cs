using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class UserChatsManager : IUserChatsManager
    {
        private readonly IUnitOfWork<IEntity> unitOfWork;

        public UserChatsManager(IUnitOfWork<IEntity> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Chat> GetUserChats(int userId)
        {
            return unitOfWork.GetRepository<UserChat>()
                .Query(chat => chat.Chat)
                .Where(user => user.UserId == userId)
                .Select(chat => chat.Chat);
        }
    }
}
