using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using Car.Data.Entities;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.UnitTests.Services
{
    public class ImageServiceTest : TestBase
    {
        private readonly IImageService imageService;
        private readonly Mock<IFileService<File>> fileService;

        public ImageServiceTest()
        {
            fileService = new Mock<IFileService<File>>();
            imageService = new ImageService(fileService.Object);
        }

        [Fact]
        public async Task UploadImageAsync_EntityAndFileNotNull_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<Data.Entities.User>();
            var imageId = Fixture.Create<string>();

            fileService
                .Setup(f => f.UploadFileAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(imageId);

            // Act
            var result = await imageService.UploadImageAsync(entity, Mock.Of<IFormFile>());

            // Assert
            result.ImageId.Should().BeEquivalentTo(imageId);
        }

        [Fact]
        public async Task UploadImageAsync_FileIsNull_ReturnsSameEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();
            var expectedImageId = entity.ImageId;

            // Act
            var result = await imageService.UploadImageAsync(entity, null);

            // Assert
            result.ImageId.Should().BeEquivalentTo(expectedImageId);
        }

        [Fact]
        public async Task UploadImageAsync_EntityIsNull_ReturnsNull()
        {
            // Arrange
            var entity = (IEntityWithImage)null;

            // Act
            var result = await imageService.UploadImageAsync(entity, Mock.Of<IFormFile>());

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteImageAsync_EntityIsNull_ReturnsNull()
        {
            // Arrange
            var entity = (IEntityWithImage)null;

            // Act
            var result = await imageService.DeleteImageAsync(entity);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteImageAsync_ImageIdIsNull_ReturnsSameEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Build<User>().With(u => u.ImageId, (string)null).Create();

            // Act
            var result = await imageService.DeleteImageAsync(entity);

            // Assert
            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async Task DeleteImageAsync_ImageIdIsValid_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();

            // Act
            var result = await imageService.DeleteImageAsync(entity);

            // Assert
            result.ImageId.Should().BeNull();
        }

        [Fact]
        public async Task UpdateImageAsync_ImageAndEntityValid_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();
            var imageId = Fixture.Create<string>();

            fileService
                .Setup(f => f.UploadFileAsync(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(imageId);

            // Act
            var result = await imageService.UpdateImageAsync(entity, Mock.Of<IFormFile>());

            // Assert
            result.ImageId.Should().BeSameAs(imageId);
        }

        [Fact]
        public async Task UpdateImageAsync_ImageIsNull_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();

            // Act
            var result = await imageService.UpdateImageAsync(entity, null);

            // Assert
            result.ImageId.Should().BeNull();
        }

        [Fact]
        public void SetImageLink_EntityHasImageId_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();
            var expectedLink = Fixture.Create<string>();

            fileService.Setup(f => f.GetFileLink(It.IsAny<string>())).Returns(expectedLink);

            // Act
            var result = imageService.SetImageLink(entity);

            // Assert
            result.ImageId.Should().BeSameAs(expectedLink);
        }

        [Fact]
        public void SetImageLink_EntityNotHaveImageId_ReturnsSameEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Build<User>().With(u => u.ImageId, (string)null).Create();

            // Act
            var result = imageService.SetImageLink(entity);

            // Assert
            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void SetImageLink_EntityIsNull_ReturnsNull()
        {
            // Arrange
            IEntityWithImage entity = null;

            // Act
            var result = imageService.SetImageLink(entity);

            // Assert
            result.Should().BeNull();
        }
    }
}
