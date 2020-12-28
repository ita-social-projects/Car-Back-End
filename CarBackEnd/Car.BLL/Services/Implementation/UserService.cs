using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;

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

        public User GetUserWithAvatarById(int userId)
        {
            var user = unitOfWork.GetRepository().GetById(userId);
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Position = user.Position,
            };
        }
    }
}
