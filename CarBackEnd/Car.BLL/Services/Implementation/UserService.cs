using System.Net;
using Car.BLL.Dto;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Collections.Generic;
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

        public User GetUserWithAvatarById(int userId)
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
