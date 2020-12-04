using System;
using System.Text;
using System.Threading.Tasks;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.BLL.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IDriveService<File> userDriveAvatars;

        public UserService(
            IUnitOfWork<User> unitOfWork,
            IConfiguration configuration,
            IDriveService<File> userDriveAvatars)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.userDriveAvatars = userDriveAvatars;
            userDriveAvatars.SetCredentials(configuration["CredentialsFile:AvatarDriveCredential"]);
        }

        public User GetUserById(int userId)
        {
            return unitOfWork.GetRepository().GetById(userId);
        }

        public async Task<User> UploadUserAvatar(int userId, IFormFile userFile)
        {
            User user = unitOfWork.GetRepository().GetById(userId);

            StringBuilder fileName = new StringBuilder();
            fileName.Append(user.Id).Append("_")
                .Append(user.Name).Append("_")
                .Append(user.Surname).Append(".jpg");

            File newFile = await userDriveAvatars.UploadFile(
                 userFile.OpenReadStream(),
                 configuration["GoogleFolders:UserAvatarFolder"],
                 fileName.ToString(),
                 "image/png");

            user.ImageAvatar = newFile.Id;

            User newUser = unitOfWork.GetRepository().Update(user);
            unitOfWork.SaveChanges();
            return newUser;
        }

        public async Task<User> DeleteUserAvatar(int userId, string userFileId)
        {
            User user = unitOfWork.GetRepository().GetById(userId);

            await userDriveAvatars.DeleteFile(userFileId);

            user.ImageAvatar = null;

            User newUser = unitOfWork.GetRepository().Update(user);
            unitOfWork.SaveChanges();
            return newUser;
        }

        public async Task<string> GetUserFileBytesById(int userId)
        {
            User user = unitOfWork.GetRepository().GetById(userId);

            byte[] buffer = await userDriveAvatars.GetFileBytesById(user.ImageAvatar);

            return Convert.ToBase64String(buffer);
        }
    }
}
