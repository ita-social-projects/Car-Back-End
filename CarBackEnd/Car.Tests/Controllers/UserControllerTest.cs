using Moq;
using Xunit;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using File = Google.Apis.Drive.v3.Data.File;
using CarBackEnd.Controllers;
using FluentAssertions.Execution;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Car.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Car.Tests.Controllers
{
    public class UserControllerTest
    {
        private Mock<IUserService> _userService;
        private Mock<IImageService<User, File>> _imageService;
        private UserController _userController;


        public UserControllerTest()
        {
            _userService = new Mock<IUserService>();
            _imageService = new Mock<IImageService<User, File>>();
            _userController = new UserController(_userService.Object, _imageService.Object);
        }

        public User GetTestUser()
        {
            return new User()
            {
                Id = 11,
                Name = "Roman",
                Surname = "Dep",
                Email = "pen@gmail.com",
                Position = "Developer",
            };
        }

        [Fact]
        public void TestGetUserById_WithRightId_ReturnsOkObjectResult()
        {
            var user = GetTestUser();
            _userService.Setup(u => u.GetUserById(user.Id)).Returns(user);

            var result = _userController.GetUserById(user.Id);
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<User>();
                ((result as OkObjectResult).Value as User).Id.Should().Be(user.Id);
                ((result as OkObjectResult).Value as User).Name.Should().Be(user.Name);
                ((result as OkObjectResult).Value as User).Surname.Should().Be(user.Surname);
                ((result as OkObjectResult).Value as User).Email.Should().Be(user.Email);
                ((result as OkObjectResult).Value as User).Position.Should().Be(user.Position);
            }
        }

        [Fact]
        public void TestGetUserById_WhenUserNotExist_ReturnsOkObjectResult()
        {
            var user = GetTestUser();

            _userService.Setup(u => u.GetUserById(user.Id)).Returns((User)null);

            var result = _userController.GetUserById(user.Id);

            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeNull();
            }
        }

        [Fact]
        public void TestGetUserWithAvatarById_WithRightId_ReturnsOkObjectResult()
        {
            var user = GetTestUser();
            _userService.Setup(u => u.GetUserWithAvatarById(user.Id)).Returns(user);

            var result = _userController.GetUserWithAvatarById(user.Id);
            using (new AssertionScope())
            {
                result.Should().BeOfType<OkObjectResult>();
                (result as OkObjectResult).Value.Should().BeOfType<User>();
                ((result as OkObjectResult).Value as User).Id.Should().Be(user.Id);
                ((result as OkObjectResult).Value as User).Name.Should().Be(user.Name);
                ((result as OkObjectResult).Value as User).Surname.Should().Be(user.Surname);
                ((result as OkObjectResult).Value as User).Email.Should().Be(user.Email);
                ((result as OkObjectResult).Value as User).Position.Should().Be(user.Position);
            }
        }

        [Fact]
        public async Task UploadUserAvatar_WhenUserNotExist()
        {
            var user = GetTestUser();

            Func<Task> action = () => (Task)_imageService.Setup(p => p.UploadImage(user.Id, new FormFile(Stream.Null, 1, 1, "1", "1")));
            await action.Should().NotThrowAsync<DefaultApplicationException>();
        }
    }
}
