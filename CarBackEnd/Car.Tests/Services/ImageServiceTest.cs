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
        private Mock<IDriveService<File>> _driveService;
        private IImageService<User, File> _imageService;
        private Mock<IRepository<User>> _repository;
        private Mock<IUnitOfWork<User>> _unitOfWork;
        private Mock<IEntityTypeStrategy<User>> _strategy;

        public ImageServiceTest()
        {
            _driveService = new Mock<IDriveService<File>>();
            _repository = new Mock<IRepository<User>>();
            _unitOfWork = new Mock<IUnitOfWork<User>>();
            _strategy = new Mock<IEntityTypeStrategy<User>>();

            _imageService = new ImageService<User>(
                _driveService.Object, _unitOfWork.Object, _strategy.Object);
        }

        public User GetTestUser()
        {
            return new User()
            {
                Id = 2,
                Name = "Tom",
                Surname = "King",
                Email = "Tom@gmail.com",
                Position = "Developer",
            };
        }

        [Fact]
        public async Task TestUploadImage()
        {
            var user = GetTestUser();

            Func<Task> action = () => _imageService.UploadImage(user.Id, new FormFile(Stream.Null, 1, 1, "1", "1"));
            await action.Should().NotThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestUploadImage_WhenFileIsNull()
        {
            var user = GetTestUser();

            Func<Task> action = () => _imageService.UploadImage(user.Id, null);
            await action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestUploadImage_WhenEntityIsNull()
        {
            var user = GetTestUser();

            Func<Task> action = () => _imageService.UploadImage(4, null);
            await action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestDeleteImage_WhenImageWasNotDeleted()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.DeleteImage(user.Id);
            await action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestDeleteImage_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.DeleteImage(4);
            await action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestGetImageBytesById_WhenUserNotExist()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.GetImageBytesById(4);
            await action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public async Task TestGetImageBytesById()
        {
            var user = GetTestUser();

            _repository.Setup(repository => repository.GetById(user.Id))
                .Returns(user);

            _unitOfWork.Setup(repository => repository.GetRepository())
                .Returns(_repository.Object);

            Func<Task> action = () => _imageService.GetImageBytesById(user.Id);
            await action.Should().NotThrowAsync();
        }
    }
}
