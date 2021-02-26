using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Models;
using Car.Domain.Models.User;

namespace Car.Domain.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);

        Task<User> UpdateUserAsync(UpdateUserModel updateUserModel);
    }
}
