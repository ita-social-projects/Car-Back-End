using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork<User> unitOfWork;

        public LoginService(IUnitOfWork<User> unitOfWork) => this.unitOfWork = unitOfWork;

        public User GetUser(string email) =>
            unitOfWork.GetRepository().Query().FirstOrDefault(p => p.Email == email);

        public User SaveUser(User user)
        {
            unitOfWork.GetRepository().Add(user);
            unitOfWork.SaveChanges();
            return user;
        }
    }
}
