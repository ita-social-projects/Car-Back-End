using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
   public interface ILoginService
   {
        Task<User> GetUserAsync(string email);

        Task<User> AddUserAsync(User user);

        Task<User> LoginAsync(User user);
   }
}
