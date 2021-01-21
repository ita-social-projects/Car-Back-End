using Car.Controllers;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Car.UnitTests.Controllers
{
    public class LoginControllerTest
    {
        private readonly Mock<ILoginService> _loginService;
        private readonly Mock<IConfiguration> _config;
        private readonly LoginController _loginController;

        public LoginControllerTest()
        {
            _loginService = new Mock<ILoginService>();
            _config = new Mock<IConfiguration>();
            _loginController = new LoginController(_config.Object, _loginService.Object);
        }

        public User GetTestUser() =>
            new User()
            {
                Id = 44,
                Name = "Peter",
                Surname = "Pen",
                Email = "pen@gmail.com",
                Position = "Developer",
            };

        public UserDto GetUserDto() =>
            new UserDto()
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

            _loginService.Setup(service => service.GetUser(user.Email))
                .Returns(user);
            _config.Setup(config => config["Jwt:Key"]).Returns(jwtToken);
            _config.Setup(config => config["Jwt:Issuer"]).Returns(jwtIssuer);

            var result = _loginController.Login(userDto);

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as UserDto)?.Name.Should().Be(user.Name);
                ((result as OkObjectResult)?.Value as UserDto)?.Surname.Should().Be(user.Surname);
                ((result as OkObjectResult)?.Value as UserDto)?.Email.Should().Be(user.Email);
                ((result as OkObjectResult)?.Value as UserDto)?.Position.Should().Be(user.Position);
                ((result as OkObjectResult)?.Value as UserDto)?.Token.Should().BeOfType<string>().And.NotBeNullOrEmpty();
            }
        }

        [Fact]
        public void TestLogin_WhenUserNotExist_ReturnsOkObjectResult()
        {
            var user = GetTestUser();
            var userDto = GetUserDto();
            const string jwtToken = "0K0j0MNJPx_9YzZXgOWz_m3k..5aI64JYq";
            const string jwtIssuer = "af2f0a21-6563-45ed-9727-6e7994722893";

            _loginService.Setup(service => service.GetUser(user.Email))
                .Returns((User)null);
            _config.Setup(config => config["Jwt:Key"]).Returns(jwtToken);
            _config.Setup(config => config["Jwt:Issuer"]).Returns(jwtIssuer);

            var result = _loginController.Login(userDto);

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<UserDto>();
                ((result as OkObjectResult)?.Value as UserDto)?.Id.Should().Be(0);
                ((result as OkObjectResult)?.Value as UserDto)?.Name.Should().Be(user.Name);
                ((result as OkObjectResult)?.Value as UserDto)?.Surname.Should().Be(user.Surname);
                ((result as OkObjectResult)?.Value as UserDto)?.Email.Should().Be(user.Email);
                ((result as OkObjectResult)?.Value as UserDto)?.Position.Should().Be(user.Position);
                ((result as OkObjectResult)?.Value as UserDto)?.Token.Should().BeOfType<string>().And.NotBeNullOrEmpty();
            }
        }
    }
}
