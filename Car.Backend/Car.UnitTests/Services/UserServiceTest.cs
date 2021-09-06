using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
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
            userService = new UserService(userRepository.Object, Mock.Of<IImageService>(), Mapper);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserByIdAsync_WhenUserExists_ReturnsUserObject(IEnumerable<User> users)
        {
            // Arrange
            var user = users.First();

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            var userToReturn = Mapper.Map<User, UserDto>(user);

            // Act
            var result = await userService.GetUserByIdAsync(user.Id);

            // Assert
            result.Should().BeEquivalentTo(userToReturn, options => options.Excluding(u => u.ImageId));
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
        public async Task GetAllUsersAsync_WhenUsersExist_ReturnsUsersEnumerable(IEnumerable<UserDto> usersDto)
        {
            // Arrange
            var users = Mapper.Map<IEnumerable<UserDto>, IEnumerable<User>>(usersDto);
            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            result.Should().BeEquivalentTo(usersDto, options => options.Excluding(u => u.ImageId));
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserImageAsync_WhenUserIsValid_ReturnsUpdatedUser(List<User> users)
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserDto.Id)
                .Create();
            users.Add(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserImageAsync(updateUserDto);

            // Assert
            result.Should().BeEquivalentTo(updateUserDto, options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserImageAsync_WhenUserIsNotValid_ReturnsNull(List<User> users)
        {
            // Arrange
            UpdateUserImageDto updateUserDto = null;
            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserImageAsync(updateUserDto);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserFcmtokenAsync_WhenUserIsValid_ReturnsUpdatedUser(List<User> users)
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserFcmtokenDto>().Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserDto.Id)
                .Create();
            users.Add(user);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserFcmtokenAsync(updateUserDto);

            // Assert
            result.Should().BeEquivalentTo(updateUserDto, options => options.ExcludingMissingMembers());
        }
    }
}
