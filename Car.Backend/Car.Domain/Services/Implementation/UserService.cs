using System.Threading.Tasks;
using Car.Data.Infrastructure;
using Car.Domain.Models;
using Car.Domain.Models.User;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IFileService<File> fileService;

        public UserService(IRepository<User> userRepository, IFileService<File> fileService)
        {
            this.userRepository = userRepository;
            this.fileService = fileService;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.ImageId != null)
            {
                user.ImageId = fileService.GetFileLinkAsync(user.ImageId);
            }

            return user;
        }

        public async Task<User> UpdateUserAsync(UpdateUserModel updateUserModel)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == updateUserModel.Id);

            if (user?.ImageId != null)
            {
                await fileService.DeleteFileAsync(user.ImageId);
                user.ImageId = null;
            }

            user.Name = updateUserModel.Name;
            user.Surname = updateUserModel.Surname;
            user.Position = updateUserModel.Position;
            user.Location = updateUserModel.Location;

            if (updateUserModel.Image != null)
            {
                user.ImageId = await fileService.UploadFileAsync(
                    updateUserModel.Image.OpenReadStream(),
                    updateUserModel.Image.Name,
                    "image/png");
            }

            await userRepository.SaveChangesAsync();
            return user;
        }
    }
}
