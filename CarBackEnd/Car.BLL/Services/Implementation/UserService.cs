using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;

        public UserService(
            IUnitOfWork<User> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetUserById(int userId)
        {
            return unitOfWork.GetRepository().GetById(userId);
        }
    }
}
