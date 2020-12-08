using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;
        private readonly IConfiguration configuration;

        public UserService(
            IUnitOfWork<User> unitOfWork,
            IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public User GetUserById(int userId)
        {
            return unitOfWork.GetRepository().GetById(userId);
        }
    }
}
