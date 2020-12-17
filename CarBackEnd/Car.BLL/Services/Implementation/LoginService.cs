using Car.DAL.Interfaces;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork<User> _unitOfWork;

        public LoginService(IUnitOfWork<User> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUser(string email)
        {
            return _unitOfWork.GetRepository().Query().Where(p => p.Email == email).FirstOrDefault();
        }

        public User SaveUser(User user)
        {
            _unitOfWork.GetRepository().Add(user);
            _unitOfWork.SaveChanges();
            return user;
        }
    }
}
