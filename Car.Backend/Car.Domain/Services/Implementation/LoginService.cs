using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using User = Car.Data.Entities.User;

namespace Car.Domain.Services.Implementation
{
    public class LoginService : ILoginService
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        private readonly GraphServiceClient graphServiceClient;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LoginService(IRepository<User> userRepository,  IMapper mapper, GraphServiceClient graphServiceClient, IHttpContextAccessor httpContextAccessor)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.graphServiceClient = graphServiceClient;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task<User> GetUserAsync(string email) =>
            userRepository.Query().FirstOrDefaultAsync(p => p.Email == email);

        public async Task<User?> AddUserAsync(User user)
        {
            if (user == null)
            {
                return null;
            }

            user.UserPreferences = new UserPreferences();
            user.UserStatistic = new UserStatistic();
            var newUser = await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            return newUser;
        }

        public async Task<UserDto> LoginAsync()
        {
            var loginUser = await UpdateUserAsync(
                await GetUserAsync(
                    httpContextAccessor
                        .HttpContext!
                        .User
                        .Claims
                        .First(c => c.Type == "preferred_username").Value)) ?? await AddUserAsync(await UpdateUserDataAsync(new User()));

            var result = mapper.Map<User, UserDto>(loginUser!);

            return result;
        }

        private async Task<User?> UpdateUserAsync(User? user)
        {
            if (user is null)
            {
                return user;
            }

            var updatedUser = await UpdateUserDataAsync(user);

            updatedUser = await userRepository.UpdateAsync(updatedUser);
            await userRepository.SaveChangesAsync();

            return updatedUser;
        }

        private async Task<User> UpdateUserDataAsync(User user)
        {
            var userData = await graphServiceClient
                .Users[
                    httpContextAccessor
                        .HttpContext!
                        .User
                        .Claims
                        .First(c => c.Type == "http://schemas.microsoft.com/identity/claims/objectidentifier")
                        .Value]
                .Request()
                .GetAsync();

            user.Name = userData.GivenName;
            user.Surname = userData.Surname;
            user.Location = userData.OfficeLocation;
            user.Position = userData.JobTitle;
            user.Email = userData.Mail ?? userData.UserPrincipalName;

            return user;
        }
    }
}
