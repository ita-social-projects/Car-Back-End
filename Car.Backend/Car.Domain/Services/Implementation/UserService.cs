using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Dto.User;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<FcmToken> fcmTokenRepository;
        private readonly IImageService imageService;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IRepository<User> userRepository, IRepository<FcmToken> fcmTokenRepository, IImageService imageService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.fcmTokenRepository = fcmTokenRepository;
            this.imageService = imageService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);
            var userDTO = mapper.Map<User, UserDto>(user);
            return userDTO;
        }

        public async Task<IEnumerable<UserEmailDto>> GetAllUsersAsync()
        {
            var users = await userRepository.Query().ToListAsync();

            return mapper.Map<List<User>, List<UserEmailDto>>(users);
        }

        public async Task<(bool IsUpdated, UserDto? UpdatedUserDto)> UpdateUserImageAsync(UpdateUserImageDto updateUserImageDto)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => updateUserImageDto.Id == u.Id);

            if (user != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != updateUserImageDto.Id)
                {
                    return (false, null);
                }

                await imageService.UpdateImageAsync(user, updateUserImageDto.Image);

                await userRepository.UpdateAsync(user);

                await userRepository.SaveChangesAsync();
            }

            return (true, mapper.Map<User, UserDto>(user!));
        }

        public async Task<UserDto> AcceptPolicyAsync()
        {
            var userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userId);

            user.IsPolicyAccepted = true;
            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(user);
        }

        public async Task<UserFcmTokenDto?> AddUserFcmtokenAsync(UserFcmTokenDto userFcmtokenDto)
        {
            var fcmToken = mapper.Map<UserFcmTokenDto, FcmToken>(userFcmtokenDto);
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            var savedFcmToken = fcmTokenRepository.Query().FirstOrDefault(token => token.Token == fcmToken.Token);
            if (savedFcmToken != null)
            {
                savedFcmToken.UserId = userId;
                await fcmTokenRepository.SaveChangesAsync();
                return mapper.Map<FcmToken, UserFcmTokenDto>(savedFcmToken);
            }

            fcmToken.UserId = userId;
            fcmToken.Id = 0;
            var addedToken = await fcmTokenRepository.AddAsync(fcmToken);
            await fcmTokenRepository.SaveChangesAsync();

            return mapper.Map<FcmToken, UserFcmTokenDto>(addedToken);
        }

        public async Task DeleteUserFcmtokenAsync(string tokenToDelete)
        {
            var fcmToken = fcmTokenRepository.Query().FirstOrDefault(token => token.Token == tokenToDelete);

            if (fcmToken != null)
            {
                fcmTokenRepository.Delete(fcmToken);
                await fcmTokenRepository.SaveChangesAsync();
            }
        }

        public async Task<UserDto> UpdateUserPhoneNumberAsync(UpdateUserNumberDto userPhone)
        {
            var user = await userRepository.Query().FirstOrDefaultAsync(u => u.Id == userPhone.Id);

            user.PhoneNumber = userPhone.PhoneNumber;
            user.IsNumberVisible = userPhone.IsNumberVisible;

            await userRepository.UpdateAsync(user);
            await userRepository.SaveChangesAsync();

            return mapper.Map<User, UserDto>(user);
        }
    }
}
