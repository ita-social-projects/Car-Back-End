using Car.DAL.Entities;
using System.Collections.Generic;

namespace Car.BLL.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);

        User GetUserWithAvatarById(int userId);
    }
}
