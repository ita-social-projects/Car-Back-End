using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using Car.WebApi.JwtConfiguration;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class LoginControllerTest
    {
        private readonly Mock<ILoginService> loginService;
        private readonly Mock<IWebTokenGenerator> webTokenGenerator;
        private readonly LoginController loginController;
        private readonly Fixture fixture;

        public LoginControllerTest()
        {
            loginService = new Mock<ILoginService>();
            webTokenGenerator = new Mock<IWebTokenGenerator>();
            loginController = new LoginController(loginService.Object, webTokenGenerator.Object);
            fixture = new Fixture();
        }

        [Fact]
        public async Task Login_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var user = fixture.Create<User>();

            loginService.Setup(service => service.LoginAsync(user))
                .ReturnsAsync(user);

            // Act
            var result = await loginController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<User>();
                ((result as OkObjectResult)?.Value as User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task Login_WhenUserNotExist_ReturnsOkObjectResult()
        {
            // Arrange
            var user = fixture.Create<User>();

            loginService.Setup(service => service.LoginAsync(user))
                .ReturnsAsync((User)null);

            // Act
            var result = await loginController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }
    }
}