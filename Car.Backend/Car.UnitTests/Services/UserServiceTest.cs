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

        [Theory]
        [AutoEntityData]
        public async Task GetUserByIdAsync_WhenUserExists_ReturnsUserObject(IEnumerable<User> users)
        {
            // Arrange
            var user = users.First();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetUserByIdAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(user, options => options.Excluding(u => u.ImageId));
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserByIdAsync_WhenUserNotExist_ReturnsNull(IEnumerable<User> users)
        {
            // Arrange
            var user = Fixture.Build<User>()
                .With(u => u.Id, users.Max(u => u.Id) + 1)
                .Create();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetUserByIdAsync(user.Id);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserAsync_WhenUserIsValid_ReturnsUpdatedUser(List<User> users)
        {
            // Arrange
            var updateUserModel = Fixture.Build<UpdateUserModel>()
                .With(u => u.Image, (IFormFile)null).Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserModel.Id)
                .Create();
            users.Add(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserAsync(updateUserModel);

            // Assert
            result.Should().BeEquivalentTo(updateUserModel, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserAsync_WhenUserIsNotValid_ReturnsNull(List<User> users)
        {
            // Arrange
            UpdateUserModel updateUserModel = null;
            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserAsync(updateUserModel);

            // Assert
            result.Should().BeNull();
        }
    }
}
