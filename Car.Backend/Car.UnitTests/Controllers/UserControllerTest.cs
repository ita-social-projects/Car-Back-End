using System.Threading.Tasks;
using AutoFixture;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Controllers;
using FluentAssertions;
using FluentAssertions.Execution;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using User = Car.Data.Entities.User;

namespace Car.UnitTests.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> userService;
        private readonly Mock<IImageService<User, File>> imageService;
        private readonly UserController userController;
        private readonly Fixture fixture;

        public UserControllerTest()
        {
            userService = new Mock<IUserService>();
            imageService = new Mock<IImageService<User, File>>();
            userController = new UserController(userService.Object, imageService.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestGetUserById_WithRightId_ReturnsOkObjectResult()
        {
            // Arrange
            var user = fixture.Create<User>();
            userService.Setup(u => u.GetUserById(user.Id)).Returns(user);

            // Act
            var result = userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<User>();
                ((result as OkObjectResult)?.Value as User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            userService.Setup(u => u.GetUserById(user.Id)).Returns((User)null);

            // Act
            var result = userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task UploadUserAvatar_WhenUserExist_ReturnsOkObjectResult()
        {
            // Arrange
            var user = fixture.Create<User>();
            var formImage = new FormImage();
            imageService.Setup(p => p.UploadImage(user.Id, formImage.Image)).Returns(Task.FromResult(user));

            // Act
            var result = await userController.UploadUserAvatar(user.Id, formImage);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<User>();
                ((result as OkObjectResult)?.Value as User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task UploadUserAvatar_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            var formImage = new FormImage();

            // Act
            imageService
                .Setup(p => p.UploadImage(user.Id, formImage.Image))
                .Returns(Task.FromResult<User>(null));

            // Assert
            using (new AssertionScope())
            {
                var result = await userController.UploadUserAvatar(user.Id, formImage);
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task DeleteUserAvatar_WhenUserExist_ReturnsDeletedUser()
        {
            // Arrange
            var user = fixture.Create<User>();
            imageService.Setup(u => u.DeleteImage(user.Id)).Returns(Task.FromResult(user));

            // Act
            var result = await userController.DeleteUserAvatar(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<User>();
                ((result as OkObjectResult)?.Value as User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task DeleteUserAvatar_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            imageService.Setup(u => u.DeleteImage(user.Id)).Returns(Task.FromResult<User>(null));

            // Act
            var result = await userController.DeleteUserAvatar(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task GetUserFileById_WhenUserExist_ReturnsStringType()
        {
            // Arrange
            var user = fixture.Create<User>();
            imageService.Setup(x => x.GetImageBytesById(user.Id)).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await userController.GetUserFileById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<string>();
            }
        }

        [Fact]
        public async Task GetUserFileById_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = fixture.Create<User>();
            imageService.Setup(x => x.GetImageBytesById(user.Id)).Returns(Task.FromResult<string>(null));

            // Act
            var result = await userController.GetUserFileById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }
    }
}
