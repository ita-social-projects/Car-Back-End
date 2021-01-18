using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);

        User GetUserWithAvatarById(int userId);
    }
}
