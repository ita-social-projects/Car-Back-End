using userEntity = Car.DAL.Entities.User;

namespace Car.BLL.Services.Interfaces
{
   public interface ILoginService
   {
        userEntity GetUser(string email);

        userEntity SaveUser(userEntity user);
   }
}
