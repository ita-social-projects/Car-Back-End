using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Models.User;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using MockQueryable.Moq;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class UserServiceTest : TestBase
    {
        private readonly Mock<IRepository<User>> userRepository;
        private readonly IUserService userService;

        public UserServiceTest()
        {
            userRepository = new Mock<IRepository<User>>();
            userService = new UserService(userRepository.Object, Mock.Of<IImageService>());
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserExists_ReturnsUserObject()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
            var user = users.First();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetUserByIdAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(user, options => options.Excluding(u => u.ImageId));
        }

        [Fact]
        public async Task GetUserByIdAsync_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var users = Fixture.CreateMany<User>();
            var user = Fixture.Build<User>()
                .With(u => u.Id, users.Max(u => u.Id) + 1)
                .Create();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetUserByIdAsync(user.Id);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserIsValid_ReturnsUpdatedUser()
        {
            // Arrange
            var updateUserModel = Fixture.Build<UpdateUserModel>()
                .With(u => u.Image, (IFormFile)null).Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserModel.Id)
                .Create();
            var users = Fixture.Create<List<User>>();
            users.Add(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserAsync(updateUserModel);

            // Assert
            result.Should().BeEquivalentTo(updateUserModel, options => options.ExcludingMissingMembers());
        }

        [Fact]
        public async Task UpdateUserAsync_WhenUserIsNotValid_ReturnsNull()
        {
            // Arrange
            UpdateUserModel updateUserModel = null;
            var users = Fixture.Create<List<User>>();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserAsync(updateUserModel);

            // Assert
            result.Should().BeNull();
        }
    }
}
