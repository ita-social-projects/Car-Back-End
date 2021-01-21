using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork<User> _unitOfWork;

        public LoginService(IUnitOfWork<User> unitOfWork) => _unitOfWork = unitOfWork;

        public User GetUser(string email) =>
            _unitOfWork.GetRepository().Query().FirstOrDefault(p => p.Email == email);

        public User SaveUser(User user)
        {
            _unitOfWork.GetRepository().Add(user);
            _unitOfWork.SaveChanges();
            return user;
        }
    }
}
