using System.Linq;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;

namespace Car.BLL.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork<User> unitOfWork;

        public LoginService(IUnitOfWork<User> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public User GetUser(string email)
        {
            return unitOfWork.GetRepository().Query().Where(p => p.Email == email).FirstOrDefault();
        }

        public User SaveUser(User user)
        {
            unitOfWork.GetRepository().Add(user);
            unitOfWork.SaveChanges();
            return user;
        }
    }
}
