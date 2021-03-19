using System.Threading.Tasks;
using Car.Data.Infrastructure;
using Car.Domain.Models.User;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IImageService imageService;

        public UserService(IRepository<User> userRepository, IImageService imageService)
        {
            this.userRepository = userRepository;
            this.imageService = imageService;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);
            //imageService.SetImageLink(user);

            return user;
        }

        public async Task<User> UpdateUserAsync(UpdateUserModel updateUserModel)
        {
            if (updateUserModel == null)
            {
                return null;
            }

            var user = await userRepository.Query().FirstOrDefaultAsync(u => updateUserModel.Id == u.Id);

            await imageService.UpdateImageAsync(user, updateUserModel.Image);

            await userRepository.SaveChangesAsync();

            return user;
        }
    }
}
