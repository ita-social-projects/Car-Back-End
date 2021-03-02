using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
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
            loginService = new LoginService(userRepository.Object, webTokenGenerator.Object);
        }

        [Fact]
        public async Task GetUser_WhenUserExists_ReturnsUserObject()
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
        public async Task AddUser_WhenUserIsValid_ReturnsUserObject()
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
    }
}
