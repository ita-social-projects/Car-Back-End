using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> userRepository;
        private readonly IWebTokenGenerator webTokenGenerator;
        private readonly IMapper mapper;

        public LoginService(IRepository<User> userRepository, IWebTokenGenerator webTokenGenerator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.webTokenGenerator = webTokenGenerator;
            this.mapper = mapper;
        }

        public Task<User> GetUserAsync(string email) =>
            userRepository.Query().FirstOrDefaultAsync(p => p.Email == email);

        public async Task<User> AddUserAsync(User user)
        {
            if (user == null)
            {
                return null;
            }

            user.UserPreferences = new UserPreferences();
            var newUser = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return newUser;
        }

        public async Task<UserDto> LoginAsync(UserDto userDto)
        {
            var user = mapper.Map<UserDto, User>(userDto);
            var loginUser = await GetUserAsync(user?.Email) ?? await AddUserAsync(user);
            if (loginUser != null)
            {
                loginUser.Token = webTokenGenerator.GenerateWebToken(loginUser);
            }

            var result = mapper.Map<User, UserDto>(loginUser);

            return result;
        }
    }
}
