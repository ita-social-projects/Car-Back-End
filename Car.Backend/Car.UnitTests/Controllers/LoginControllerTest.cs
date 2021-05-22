using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class LoginControllerTest : TestBase
    {
        private readonly Mock<ILoginService> loginService;
        private readonly LoginController loginController;

        public LoginControllerTest()
        {
            loginService = new Mock<ILoginService>();
            loginController = new LoginController(loginService.Object);
        }

        [Fact]
        public async Task Login_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var user = Fixture.Create<UserDto>();

            loginService.Setup(service => service.LoginAsync(user))
                .ReturnsAsync(user);

            // Act
            var result = await loginController.Login(user);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as UserDto)?.Name.Should().Be(user.Name);
            }
        }
    }
}