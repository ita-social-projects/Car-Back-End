using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class LoginServiceTest
    {
        private readonly ILoginService loginService;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Fixture fixture;

        public LoginServiceTest()
        {
            userRepository = new Mock<IRepository<User>>();

            loginService = new LoginService(userRepository.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public async Task GetUser_WhenUserExists_ReturnsUserObject()
        {
            // Arrange
            var user = fixture.Create<User>();
            var users = fixture.Create<List<User>>();
            users.Add(user);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable);

            // Act
            var result = await loginService.GetUserAsync(user.Email);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task AddUser_WhenUserIsValid_ReturnsUserObject()
        {
            // Arrange
            var user = fixture.Create<User>();
            userRepository.Setup(r => r.AddAsync(user))
               .ReturnsAsync(user);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeEquivalentTo(user);
        }
    }
}
