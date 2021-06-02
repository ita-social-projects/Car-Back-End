using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Models.User;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Car.UnitTests.Controllers
{
    public class UserControllerTest : TestBase
    {
        private readonly Mock<IUserService> userService;
        private readonly UserController userController;

        public UserControllerTest()
        {
            userService = new Mock<IUserService>();
            userController = new UserController(userService.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserById_WhenUserExists_ReturnsOkObjectResult(User user)
        {
            // Arrange
            userService.Setup(service => service.GetUserByIdAsync(user.Id)).ReturnsAsync(user);

            // Act
            var result = await userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(user);
            }
        }

        [Fact]
        public async Task UpdateUser_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var updateUserModel = Fixture.Build<UpdateUserModel>()
                .With(u => u.Image, (IFormFile)null).Create();
            var expectedUser = Mapper.Map<UpdateUserModel, User>(updateUserModel);

            userService.Setup(service => service.UpdateUserAsync(updateUserModel)).ReturnsAsync(expectedUser);

            // Act
            var result = await userController.UpdateUser(updateUserModel);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedUser);
            }
        }
    }
}
