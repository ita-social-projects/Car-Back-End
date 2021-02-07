using System.Net;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;

        public UserService(IUnitOfWork<User> unitOfWork) => this.unitOfWork = unitOfWork;

        public User GetUserById(int userId)
        {
            var user = unitOfWork.GetRepository().GetById(userId);
            if (user == null)
            {
                throw new Exceptions.DefaultApplicationException($"This user id - {userId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            return user;
        }
    }
}
