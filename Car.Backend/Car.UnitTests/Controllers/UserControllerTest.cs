﻿using System.Collections.Generic;
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
        public async Task GetAllUsers_WhenUsersExist_ReturnsOkObjectResult(List<UserDto> users)
        {
            // Arrange
            userService.Setup(service => service.GetAllUsersAsync()).ReturnsAsync(users);

            // Act
            var result = await userController.GetAllUsers();

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(users);
            }
        }

        [Fact]
        public async Task UpdateUserImage_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var expectedUser = Mapper.Map<UpdateUserImageDto, UserDto>(updateUserDto);

            userService.Setup(service => service.UpdateUserImageAsync(updateUserDto)).ReturnsAsync(expectedUser);

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
        public async Task UpdateUserFcmtoken_WhenUserExists_ReturnsOkObjectResult()
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserFcmtokenDto>().Create();
            var expectedUser = Mapper.Map<UpdateUserFcmtokenDto, UserDto>(updateUserDto);

            userService.Setup(service => service.UpdateUserFcmtokenAsync(updateUserDto)).ReturnsAsync(expectedUser);

            // Act
            var result = await userController.UpdateUserFcmtoken(updateUserDto);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().Be(expectedUser);
            }
        }
    }
}
