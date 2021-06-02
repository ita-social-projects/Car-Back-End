using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class LoginServiceTest : TestBase
    {
        private readonly ILoginService loginService;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IWebTokenGenerator> webTokenGenerator;

        public LoginServiceTest()
        {
            userRepository = new Mock<IRepository<User>>();
            webTokenGenerator = new Mock<IWebTokenGenerator>();
            loginService = new LoginService(userRepository.Object, webTokenGenerator.Object, Mapper);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserAsync_WhenUserExists_ReturnsUserObject(User user, List<User> users)
        {
            // Arrange
            users.Add(user);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(user.Email);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserAsync_WhenUserNotExist_ReturnsNull(List<User> users)
        {
            // Arrange
            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(null);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserAsync_WhenUserIsValid_ReturnsUserObject(User user)
        {
            // Arrange
            userRepository.Setup(r => r.AddAsync(user))
               .ReturnsAsync(user);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserAsync_WhenUserIsNotValid_ReturnsNull(User user)
        {
            // Arrange
            userRepository.Setup(r => r.AddAsync(user))
                .ReturnsAsync((User)null);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task LoginAsync_WhenUserExists_ReturnsUserWithToken(IEnumerable<User> users, string token)
        {
            // Arrange
            var user = users.First();
            var userDto = Mapper.Map<UserDto>(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            webTokenGenerator.Setup(w => w.GenerateWebToken(user)).Returns(token);

            // Act
            var result = await loginService.LoginAsync(userDto);

            // Assert
            result.Token.Should().BeSameAs(token);
        }

        [Theory]
        [AutoEntityData]
        public async Task LoginAsync_WhenUserNotExist_ReturnsUserWithToken(User user, string token)
        {
            // Arrange
            var users = new List<User>();
            var userDto = Mapper.Map<UserDto>(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            webTokenGenerator.Setup(w => w.GenerateWebToken(user)).Returns(token);
            userRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await loginService.LoginAsync(userDto);

            // Assert
            result.Token.Should().BeSameAs(token);
        }

        [Theory]
        [AutoEntityData]
        public async Task LoginAsync_WhenUserIsNotValid_ReturnsNull(IEnumerable<User> users)
        {
            // Arrange
            User user = null;
            var userDto = Mapper.Map<UserDto>(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            userRepository.Setup(repo => repo.AddAsync(user)).ReturnsAsync(user);

            // Act
            var result = await loginService.LoginAsync(userDto);

            // Assert
            result.Should().BeNull();
        }
    }
}
