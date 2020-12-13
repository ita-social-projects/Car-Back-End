using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);
    }
}
