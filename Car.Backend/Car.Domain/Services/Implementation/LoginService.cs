using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> userRepository;

        public LoginService(IRepository<User> userRepository) => this.userRepository = userRepository;

        public Task<User> GetUserAsync(string email) =>
            userRepository.Query().FirstOrDefaultAsync(p => p.Email == email);

        public async Task<User> AddUserAsync(User user)
        {
            user.UserPreferences = new UserPreferences();
            var newUser = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> LoginAsync(User user)
        {
            return await GetUserAsync(user.Email) ?? await AddUserAsync(user);
        }
    }
}
