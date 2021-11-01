using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using MockQueryable.Moq;
using Moq;
using Xunit;
using User = Car.Data.Entities.User;

namespace Car.UnitTests.Services
{
    public class LoginServiceTest : TestBase
    {
        private readonly ILoginService loginService;
        private readonly Mock<IRepository<User>> userRepository;
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly Mock<IAuthenticationProvider> authenticationProvider;
        private readonly Mock<IHttpProvider> httpProvider;
        private readonly Mock<GraphServiceClient> graphServiceClient;

        public LoginServiceTest()
        {
            userRepository = new Mock<IRepository<User>>();
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            authenticationProvider = new Mock<IAuthenticationProvider>();
            httpProvider = new Mock<IHttpProvider>();
            graphServiceClient = new Mock<GraphServiceClient>("https://graph.microsoft.com/v1.0", authenticationProvider.Object, httpProvider.Object);
            loginService = new LoginService(userRepository.Object, Mapper, graphServiceClient.Object, httpContextAccessor.Object);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserAsync_WhenUserExists_ReturnsUserObject(User user, List<User> users)
        {
            // Arrange
            users.Add(user);

            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(user.Email);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoEntityData]
        public async Task GetUserAsync_WhenUserNotExist_ReturnsNull(List<User> users)
        {
            // Arrange
            userRepository.Setup(r => r.Query())
                .Returns(users.AsQueryable().BuildMock().Object);

            // Act
            var result = await loginService.GetUserAsync(null);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserAsync_WhenUserIsValid_ReturnsUserObject(User user)
        {
            // Arrange
            userRepository.Setup(r => r.AddAsync(user))
               .ReturnsAsync(user);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeEquivalentTo(user);
        }

        [Theory]
        [AutoEntityData]
        public async Task AddUserAsync_WhenUserIsNotValid_ReturnsNull(User user)
        {
            // Arrange
            userRepository.Setup(r => r.AddAsync(user))
                .ReturnsAsync((User)null);

            // Act
            var result = await loginService.AddUserAsync(user);

            // Assert
            result.Should().BeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task LoginAsync_WhenUserExists_ReturnsUser(IEnumerable<User> users)
        {
            // Arrange
            var user = users.First();
            var claims = new List<Claim>()
            {
                new("preferred_username", user.Email),
                new("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
            };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var userFromGraph = Fixture.Build<Microsoft.Graph.User>()
                .OmitAutoProperties()
                .With(u => u.GivenName)
                .With(u => u.Surname)
                .With(u => u.OfficeLocation)
                .With(u => u.JobTitle)
                .With(u => u.HireDate)
                .With(u => u.Mail)
                .With(u => u.UserPrincipalName)
                .With(u => u.MobilePhone)
                .Create();
            graphServiceClient.Setup(g => g
                    .Users[It.IsAny<string>()]
                    .Request()
                    .GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userFromGraph);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            userRepository.Setup(repo => repo.UpdateAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await loginService.LoginAsync();

            // Assert
            result.Should().NotBeNull();
        }

        [Theory]
        [AutoEntityData]
        public async Task LoginAsync_WhenUserNotExist_ReturnsUser(User user)
        {
            // Arrange
            var users = new List<User>();
            var claims = new List<Claim>()
            {
                new("preferred_username", user.Email),
                new("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
            };
            httpContextAccessor.Setup(h => h.HttpContext.User.Claims).Returns(claims);
            var userFromGraph = Fixture.Build<Microsoft.Graph.User>()
                .OmitAutoProperties()
                .With(u => u.GivenName)
                .With(u => u.Surname)
                .With(u => u.OfficeLocation)
                .With(u => u.JobTitle)
                .With(u => u.HireDate)
                .With(u => u.Mail)
                .With(u => u.UserPrincipalName)
                .With(u => u.MobilePhone)
                .Create();
            graphServiceClient.Setup(g => g
                    .Users[It.IsAny<string>()]
                    .Request()
                    .GetAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(userFromGraph);

            userRepository.Setup(repo => repo.Query()).Returns(users.AsQueryable().BuildMock().Object);
            userRepository.Setup(repo => repo.AddAsync(It.IsAny<User>())).ReturnsAsync(user);

            // Act
            var result = await loginService.LoginAsync();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
