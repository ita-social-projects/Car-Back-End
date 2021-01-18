using Moq;
using Xunit;
using FluentAssertions.Execution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Car.Controllers;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using User = Car.Data.Entities.User;

namespace Car.UnitTests.Controllers
{
    public class UserControllerTest
    {
        private readonly Mock<IUserService> _userService;
        private readonly Mock<IImageService<Data.Entities.User, File>> _imageService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _imageService = new Mock<IImageService<Data.Entities.User, File>>();
            _userController = new UserController(_userService.Object, _imageService.Object);
        }

        public Data.Entities.User GetTestUser() =>
            new Data.Entities.User()
            {
                Id = It.IsAny<int>(),
                Name = It.IsAny<string>(),
            };

        [Fact]
        public void TestGetUserById_WithRightId_ReturnsOkObjectResult()
        {
            // Arrange
            var user = GetTestUser();
            _userService.Setup(u => u.GetUserById(user.Id)).Returns(user);

            // Act
            var result = _userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.User>();
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = GetTestUser();
            _userService.Setup(u => u.GetUserById(user.Id)).Returns((Data.Entities.User)null);

            // Act
            var result = _userController.GetUserById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public void TestGetUserWithAvatarById_WithRightId_ReturnsOkObjectResult()
        {
            // Arrange
            var user = GetTestUser();
            _userService.Setup(u => u.GetUserWithAvatarById(user.Id)).Returns(user);

            // Act
            var result = _userController.GetUserWithAvatarById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.User>();
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task UploadUserAvatar_WhenUserExist_ReturnsOkObjectResult()
        {
            // Arrange
            var user = GetTestUser();
            var formImage = new FormImage();
            _imageService.Setup(p => p.UploadImage(user.Id, formImage.Image)).Returns(Task.FromResult(new Data.Entities.User()));

            // Act
            var result = await _userController.UploadUserAvatar(user.Id, formImage);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.User>();
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task UploadUserAvatar_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = GetTestUser();
            var formImage = new FormImage();

            // Act
            _imageService
                .Setup(p => p.UploadImage(user.Id, formImage.Image))
                .Returns(Task.FromResult<Data.Entities.User>(null));

            // Assert
            using (new AssertionScope())
            {
                var result = await _userController.UploadUserAvatar(user.Id, formImage);
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }

        [Fact]
        public async Task DeleteUserAvatar_WhenUserExist_ReturnsDeletedUser()
        {
            // Arrange
            var user = GetTestUser();
            _imageService.Setup(u => u.DeleteImage(user.Id)).Returns(Task.FromResult(new Data.Entities.User()));

            // Act
            var result = await _userController.DeleteUserAvatar(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeOfType<Data.Entities.User>();
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Id.Should().Be(user.Id);
                ((result as OkObjectResult)?.Value as Data.Entities.User)?.Name.Should().Be(user.Name);
            }
        }

        [Fact]
        public async Task DeleteUserAvatar_WhenUserNotExist_ReturnsNull()
        {
            // Arrange
            var user = GetTestUser();
            _imageService.Setup(u => u.DeleteImage(user.Id)).Returns(Task.FromResult<Data.Entities.User>(null));

            // Act
            var result = await _userController.DeleteUserAvatar(user.Id);

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
            var user = GetTestUser();
            _imageService.Setup(x => x.GetImageBytesById(user.Id)).Returns(Task.FromResult(string.Empty));

            // Act
            var result = await _userController.GetUserFileById(user.Id);

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
            var user = GetTestUser();
            _imageService.Setup(x => x.GetImageBytesById(user.Id)).Returns(Task.FromResult<string>(null));

            // Act
            var result = await _userController.GetUserFileById(user.Id);

            // Assert
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult)?.Value.Should().BeNull();
            }
        }
    }
}
