using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public UserService(IRepository<User> userRepository, IImageService imageService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);

            return mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.Query().ToListAsync();

            return mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto?> UpdateUserImageAsync(UpdateUserImageDto updateUserImageDto)
        {
            if (updateUserImageDto == null)
            {
                return null;
            }

            var user = await userRepository.Query().FirstOrDefaultAsync(u => updateUserImageDto.Id == u.Id);

            await imageService.UpdateImageAsync(user, updateUserImageDto.Image);

            await userRepository.UpdateAsync(user);

            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto?> UpdateUserFcmtokenAsync(UpdateUserFcmtokenDto updateUserFcmtokenDto)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => updateUserFcmtokenDto.Id == u.Id);

            user.FCMToken = updateUserFcmtokenDto.Fcmtoken;

            await userRepository.UpdateAsync(user);

            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(user);
        }
    }
}
