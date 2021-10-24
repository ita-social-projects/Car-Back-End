using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
   public interface ILoginService
   {
      Task<User> GetUserAsync(string email);

      Task<User?> AddUserAsync(User user);

      Task<UserDto> LoginAsync();
   }
}
