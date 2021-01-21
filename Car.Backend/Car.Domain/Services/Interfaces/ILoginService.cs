using Car.Data.Entities;
using userEntity = Car.Data.Entities.User;

namespace Car.Domain.Services.Interfaces
{
   public interface ILoginService
   {
        User GetUser(string email);

        User SaveUser(User user);
   }
}
