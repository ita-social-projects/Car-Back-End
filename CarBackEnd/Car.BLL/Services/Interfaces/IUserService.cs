using Car.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int userId);

        Task<User> UploadUserAvatar(int userId, IFormFile userFile);

        Task<User> DeleteUserAvatar(int userId, string userFileId);

        Task<string> GetUserFileBytesById(int userId);
    }
}
