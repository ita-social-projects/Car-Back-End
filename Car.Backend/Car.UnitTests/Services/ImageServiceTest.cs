using System;
using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Exceptions;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.UnitTests.Services
{
    public class ImageServiceTest
    {
        private readonly IImageService<User, File> imageService;
        private readonly Mock<IRepository<User>> repository;
        private readonly Mock<IUnitOfWork<User>> unitOfWork;
        private readonly Fixture fixture;

        public ImageServiceTest()
        {
            var driveService = new Mock<IDriveService<File>>();
            var strategy = new Mock<IEntityTypeStrategy<User>>();

            repository = new Mock<IRepository<User>>();
            unitOfWork = new Mock<IUnitOfWork<User>>();

            imageService = new ImageService<User>(
                driveService.Object, unitOfWork.Object, strategy.Object);

            fixture = new Fixture();

            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void TestUploadImage()
        {
            var user = fixture.Create<User>();

            Func<Task> action = () => imageService
                .UploadImage(user.Id, fixture.Create<FormFile>());

            action.Should().NotThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestUploadImage_WhenFileIsNull()
        {
            var user = fixture.Create<User>();

            Func<Task> action = () => imageService.UploadImage(user.Id, null);
            action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestUploadImage_WhenEntityIsNull()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Func<Task> action = () => imageService.UploadImage(It.IsNotIn(user.Id), null);
            action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestDeleteImage_WhenImageWasNotDeleted()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Func<Task> action = () => imageService.DeleteImage(user.Id);
            action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestDeleteImage_WhenUserNotExist()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Func<Task> action = () => imageService.DeleteImage(It.IsNotIn(user.Id));
            action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetImageBytesById_WhenUserNotExist()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Func<Task> action = () => imageService.GetImageBytesById(4);
            action.Should().ThrowAsync<DefaultApplicationException>();
        }

        [Fact]
        public void TestGetImageBytesById()
        {
            var user = fixture.Create<User>();

            repository.Setup(r => r.GetById(user.Id))
                .Returns(user);

            unitOfWork.Setup(r => r.GetRepository())
                .Returns(repository.Object);

            Func<Task> action = () => imageService.GetImageBytesById(user.Id);

            action.Should().NotThrowAsync();
        }
    }
}