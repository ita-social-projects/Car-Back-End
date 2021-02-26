using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Interfaces;
using Car.Domain.Models;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.EntityFrameworkCore;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork<User> unitOfWork;
        private readonly IFileService<File> fileService;
        private readonly IMapper mapper;

        public UserService(IUnitOfWork<User> unitOfWork, IFileService<File> fileService, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.fileService = fileService;
            this.mapper = mapper;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await unitOfWork.GetRepository().Query().FirstOrDefaultAsync(u => u.Id == userId);

            if (user?.ImageId != null)
            {
                user.ImageId = fileService.GetFileLinkAsync(user.ImageId);
            }

            return user;
        }

        public async Task<User> UpdateUserAsync(UpdateUserModel updateUserModel)
        {
            var user = await unitOfWork.GetRepository().Query().FirstOrDefaultAsync(u => u.Id == updateUserModel.Id);

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

            await unitOfWork.SaveChangesAsync();
            return user;
        }
    }
}
