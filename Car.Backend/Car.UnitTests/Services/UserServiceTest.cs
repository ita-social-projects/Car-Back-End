using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        private readonly Mock<IRepository<FcmToken>> fcmTokenRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly IUserService userService;

        public UserServiceTest()
        {
            userRepository = new Mock<IRepository<User>>();
            fcmTokenRepository = new Mock<IRepository<FcmToken>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            userService = new UserService(userRepository.Object, fcmTokenRepository.Object, Mock.Of<IImageService>(), Mapper, httpContextAccessor.Object);
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
        public async Task GetAllUsersAsync_WhenUsersExist_ReturnsUsersEnumerable(IEnumerable<UserEmailDto> usersDto)
        {
            // Arrange
            var users = Mapper.Map<IEnumerable<UserEmailDto>, IEnumerable<User>>(usersDto);
            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.GetAllUsersAsync();

            // Assert
            result.Should().BeEquivalentTo(usersDto);
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
        public async Task AddUserFcmtokenAsync_WhenTokenIsValid_ReturnsAddedToken(UserFcmTokenDto userFCMTokenDto, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var fcmToken = Mapper.Map<UserFcmTokenDto, FcmToken>(userFCMTokenDto);
            fcmTokenRepository.Setup(repo => repo.AddAsync(It.IsAny<FcmToken>())).ReturnsAsync(fcmToken);

            // Act
            var result = await userService.AddUserFcmtokenAsync(userFCMTokenDto);

            // Assert
            result.Should().BeEquivalentTo(userFCMTokenDto);
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteUserFcmtokenAsync_WhenUserIsOwner_ExecuteOnce(FcmToken fcmToken, List<FcmToken> fcmTokens)
        {
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, fcmToken.UserId.ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            fcmTokens.Add(fcmToken);
            fcmTokenRepository.Setup(repo => repo.Query())
                .Returns(fcmTokens.AsQueryable().BuildMock().Object);

            // Act
            await userService.DeleteUserFcmtokenAsync(fcmToken.Token);

            // Assert
            fcmTokenRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteUserFcmtokenAsync_WhenUserIsOwner_ExecuteNever(FcmToken fcmToken, List<FcmToken> fcmTokens)
        {
            // Arrange
            // Arrange
            var claims = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, (fcmToken.UserId + 1).ToString()) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            fcmTokens.Add(fcmToken);
            fcmTokenRepository.Setup(repo => repo.Query())
                .Returns(fcmTokens.AsQueryable().BuildMock().Object);

            // Act
            await userService.DeleteUserFcmtokenAsync(fcmToken.Token);

            // Assert
            fcmTokenRepository.Verify(repo => repo.SaveChangesAsync(), Times.Never());
        }
    }
}
