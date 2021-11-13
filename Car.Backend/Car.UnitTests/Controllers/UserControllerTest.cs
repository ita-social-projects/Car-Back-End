using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using Car.Domain.Dto;
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
        public async Task GetUserById_WhenUserExists_ReturnsOkObjectResult(UserDto user)
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

        [Theory]
        [AutoEntityData]
        public async Task GetUserByEmail_WhenUserExist_ReturnsOkObjectResult(UserDto user)
        {
            // Arrange
            userService.Setup(service => service.GetUserByEmailAsync(user.Email)).ReturnsAsync(user);

            // Act
            var result = await userController.GetUserByEmail(user.Email);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(user);
            }
        }

        [Fact]
        public async Task UpdateUserImage_WhenUserExistsIsAllowed_ReturnsOkObjectResult()
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var expectedUser = Mapper.Map<UpdateUserImageDto, UserDto>(updateUserDto);

            userService.Setup(service => service.UpdateUserImageAsync(updateUserDto)).ReturnsAsync((true, expectedUser));

            // Act
            var result = await userController.UpdateUserImage(updateUserDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedUser);
            }
        }

        [Fact]
        public async Task UpdateUserImage_WhenUserExistsIsAllowed_ReturnsForbidResult()
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var expectedUser = Mapper.Map<UpdateUserImageDto, UserDto>(updateUserDto);

            userService.Setup(service => service.UpdateUserImageAsync(updateUserDto)).ReturnsAsync((false, null));

            // Act
            var result = await userController.UpdateUserImage(updateUserDto);

            // Assert
            result.Should().BeOfType<ForbidResult>();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserFcmtoken_WhenUserExists_ReturnsOkObjectResult(UserFcmTokenDto userFCMTokenDto)
        {
            // Arrange
            var expectedFCM = userFCMTokenDto;
            userService.Setup(service => service.AddUserFcmtokenAsync(userFCMTokenDto)).ReturnsAsync(expectedFCM);

            // Act
            var result = await userController.AddUserFcmtoken(userFCMTokenDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedFCM);
            }
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteUserFcmtoken_WhenIsAllowed_ReturnsOkObjectResult(string token)
        {
            // Act
            var result = await userController.DeleteUserFcmtoken(token);

            // Assert
            result.Should().BeOfType<OkResult>();
        }
    }
}
