using System;
using System.IO;
using Car.BLL.Exceptions;
using Car.BLL.Services.Implementation;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Moq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Tests.Services
{
    public class ImageServiceTest
    {
        private readonly IImageService<User, File> _imageService;
        private readonly Mock<IRepository<User>> _repository;
        private readonly Mock<IUnitOfWork<User>> _unitOfWork;

        public ImageServiceTest()
        {
            var driveService = new Mock<IDriveService<File>>();
            var strategy = new Mock<IEntityTypeStrategy<User>>();

            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();

            _imageService = new ImageService<User>(
                driveService.Object, _unitOfWork.Object, strategy.Object);
        }

        public User GetTestUser() =>
            new User()
            {
                Id = 2,
                Name = "Tom",
                Surname = "King",
                Email = "Tom@gmail.com",
                Position = "Developer",
            };

        [Fact]
        public Task TestUploadImage()
        {
            var user = GetTestUser();

            Func<Task> action = () => _imageService
                .UploadImage(user.Id, new FormFile(
                    Stream.Null,
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()));

            return action.Should().NotThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestUploadImage_WhenFileIsNull()
        {
            var user = GetTestUser();

            Func<Task> action = () => _imageService.UploadImage(user.Id, null);
            return action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestUploadImage_WhenEntityIsNull()
        {
            Func<Task> action = () => _imageService.UploadImage(4, null);
            return action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestDeleteImage_WhenImageWasNotDeleted()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.DeleteImage(user.Id);
            return action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestDeleteImage_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.DeleteImage(4);
            return action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestGetImageBytesById_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.GetImageBytesById(4);
            return action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public Task TestGetImageBytesById()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.GetImageBytesById(user.Id);
            return action.Should().NotThrowAsync();
        }
    }
}