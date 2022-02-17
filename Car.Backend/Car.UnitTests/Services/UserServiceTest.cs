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
        public async Task UpdateUserImageAsync_WhenUserIsValidAndIsAllowed_ReturnsUpdatedUser(List<User> users)
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserDto.Id)
                .Create();
            users.Add(user);

            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserImageAsync(updateUserDto);

            // Assert
            result.Should().BeEquivalentTo((true, updateUserDto), options => options.ExcludingMissingMembers());
        }

        [Theory]
        [AutoEntityData]
        public async Task UpdateUserImageAsync_WhenUserIsValidAndIsNotAllowed_ReturnsFalse(List<User> users)
        {
            // Arrange
            var updateUserDto = Fixture.Build<UpdateUserImageDto>()
                .With(u => u.Image, (IFormFile)null).Create();
            var user = Fixture.Build<User>().With(u => u.Id, updateUserDto.Id)
                .Create();
            users.Add(user);

            var currentUser = Fixture.Build<User>().With(u => u.Id, user.Id + 1).Create();
            var claims = new List<Claim>() { new("preferred_username", currentUser.Email) };
            users.Add(currentUser);
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.UpdateUserImageAsync(updateUserDto);

            // Assert
            result.Should().Be((false, null));
        }

        [Theory]
        [AutoEntityData]
        public async Task AcceptPolicyAsync_WhenUserIsValid_ReturnsTrue(List<User> users)
        {
            // Arrange
            var user = Fixture.Build<User>().With(u => u.IsPolicyAccepted, false).Create();
            users.Add(user);

            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await userService.AcceptPolicyAsync();
            user.IsPolicyAccepted = true;

            // Assert
            result.Should().Be(Mapper.Map<User, UserDto>(user));
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserFcmtokenAsync_WhenTokenIsValid_ReturnsAddedToken(UserFcmTokenDto userFCMTokenDto, User user)
        {
            // Arrange
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            var fcmToken = Mapper.Map<UserFcmTokenDto, FcmToken>(userFCMTokenDto);
            fcmTokenRepository.Setup(repo => repo.AddAsync(It.IsAny<FcmToken>())).ReturnsAsync(fcmToken);

            // Act
            var result = await userService.AddUserFcmtokenAsync(userFCMTokenDto);

            // Assert
            result.Should().BeEquivalentTo(userFCMTokenDto);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserFcmtokenAsync_WhenTokenExists_ReturnsUpdatedToken(IEnumerable<FcmToken> fcmTokens, User user)
        {
            // Arrange
            var fcmToken = fcmTokens.First();
            var userFCMTokenDto = Mapper.Map<FcmToken, UserFcmTokenDto>(fcmToken);
            var claims = new List<Claim>() { new("preferred_username", user.Email) };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            userRepository.Setup(rep => rep.Query()).Returns(new[] { user }.AsQueryable());
            fcmTokenRepository.Setup(repo => repo.Query()).Returns(fcmTokens.AsQueryable().BuildMock().Object);
            fcmTokenRepository.Setup(repo => repo.AddAsync(It.IsAny<FcmToken>())).ReturnsAsync(fcmToken);

            // Act
            var result = await userService.AddUserFcmtokenAsync(userFCMTokenDto);

            // Assert
            result.Should().BeEquivalentTo(userFCMTokenDto);
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteUserFcmtokenAsync_WhenTokenExists_ExecuteOnce(FcmToken fcmToken, List<FcmToken> fcmTokens)
        {
            // Arrange
            fcmTokens.Add(fcmToken);
            fcmTokenRepository.Setup(repo => repo.Query())
                .Returns(fcmTokens.AsQueryable().BuildMock().Object);

            // Act
            await userService.DeleteUserFcmtokenAsync(fcmToken.Token);

            // Assert
            fcmTokenRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once());
        }
    }
}
