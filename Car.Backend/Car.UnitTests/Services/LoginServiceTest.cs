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

        [Fact]
        public async Task GetUserAsync_WhenUserExists_ReturnsUserObject()
        {
            // Arrange
            var user = Fixture.Create<User>();
            var users = Fixture.Create<List<User>>();
            users.Add(user);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(user.Email);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUserAsync_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var users = Fixture.Create<List<User>>();

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(null);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task AddUserAsync_WhenUserIsValid_ReturnsUserObject()
        {
            // Arrange
            var user = Fixture.Create<User>();
            userRepository.Setup(r => r.AddAsync(user))
               .ReturnsAsync(user);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task AddUserAsync_WhenUserIsNotValid_ReturnsNull()
        {
            // Arrange
            var user = Fixture.Create<User>();
            userRepository.Setup(r => r.AddAsync(user))
                .ReturnsAsync((User)null);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task LoginAsync_WhenUserExists_ReturnsUserWithToken()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
            var user = users.First();
            var token = Fixture.Create<string>();
            var userDto = Mapper.Map<UserDto>(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            webTokenGenerator.Setup(w => w.GenerateWebToken(user)).Returns(token);

            // Act
            var result = await loginService.LoginAsync(userDto);

            // Assert
            result.Token.Should().BeSameAs(token);
        }

        [Fact]
        public async Task LoginAsync_WhenUserNotExist_ReturnsUserWithToken()
        {
            // Arrange
            var users = new List<User>();
            var user = Fixture.Create<User>();
            var token = Fixture.Create<string>();
            var userDto = Mapper.Map<UserDto>(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            webTokenGenerator.Setup(w => w.GenerateWebToken(user)).Returns(token);
            userRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await loginService.LoginAsync(userDto);

            // Assert
            result.Token.Should().BeSameAs(token);
        }

        [Fact]
        public async Task LoginAsync_WhenUserIsNotValid_ReturnsNull()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
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
