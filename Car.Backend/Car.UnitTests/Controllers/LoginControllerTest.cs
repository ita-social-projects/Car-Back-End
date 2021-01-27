using Car.Data.Entities;
using Car.Domain.Configurations;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using Car.WebApi.JwtConfiguration;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class LoginControllerTest
    {
        private readonly Mock<ILoginService> loginService;
        private readonly Mock<IOptions<Jwt>> options;
        private readonly LoginController loginController;

        public LoginControllerTest()
        {
            loginService = new Mock<ILoginService>();
            options = new Mock<IOptions<Jwt>>();
            loginController = new LoginController(loginService.Object, options.Object);
        }

        public User GetTestUser() =>
            new User
            {
                Id = 44,
                Name = "Peter",
                Surname = "Pen",
                Email = "pen@gmail.com",
                Position = "Developer",
            };

        public UserDto GetUserDto() =>
            new UserDto
            {
                Id = 44,
                Name = "Peter",
                Surname = "Pen",
                Email = "pen@gmail.com",
                Position = "Developer",
            };

        [Fact]
        public void TestLogin_WhenUserExist_ReturnsOkObjectResult()
        {
            var user = GetTestUser();
            var userDto = GetUserDto();
            const string jwtToken = "0K0j0MNJPx_9YzZXgOWz_m3k..5aI64JYq";
            const string jwtIssuer = "af2f0a21-6563-45ed-9727-6e7994722893";

            loginService.Setup(service => service.GetUser(user.Email))
                .Returns(user);
            options.Setup(option => option.Value.Key)
                .Returns(jwtToken);
            options.Setup(option => option.Value.Issuer).Returns(jwtIssuer);

            var result = loginController.Login(userDto);

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as UserDto)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public void TestLogin_WhenUserNotExist_ReturnsOkObjectResult()
        {
            var user = GetTestUser();
            var userDto = GetUserDto();
            const string jwtToken = "0K0j0MNJPx_9YzZXgOWz_m3k..5aI64JYq";
            const string jwtIssuer = "af2f0a21-6563-45ed-9727-6e7994722893";

            loginService.Setup(service => service.GetUser(user.Email))
                .Returns((User)null);
            options.Setup(option => option.Value.Key).Returns(jwtToken);
            options.Setup(option => option.Value.Issuer).Returns(jwtIssuer);

            var result = loginController.Login(userDto);

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(0);
                ((result as OkObjectResult)?.Value as UserDto)?.Name.Should().Be(user.Name);
            }
        }
    }
}