using System.IO;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.Xunit2;
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

        [Theory]
        [AutoData]
        public async Task UploadImageAsync_EntityAndFileNotNull_ReturnsUpdatedEntity(string imageId)
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<Data.Entities.User>();
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
        public void DeleteImage_EntityIsNull_ReturnsNull()
        {
            // Arrange
            var entity = (IEntityWithImage)null;

            // Act
            var result = imageService.DeleteImage(entity);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public void DeleteImage_ImageIdIsNull_ReturnsSameEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Build<User>().With(u => u.ImageId, (string)null).Create();

            // Act
            var result = imageService.DeleteImage(entity);

            // Assert
            result.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public void DeleteImage_ImageIdIsValid_ReturnsUpdatedEntity()
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();

            // Act
            var result = imageService.DeleteImage(entity);

            // Assert
            result.ImageId.Should().BeNull();
        }

        [Theory]
        [AutoData]
        public async Task UpdateImageAsync_ImageAndEntityValid_ReturnsUpdatedEntity(string imageId)
        {
            // Arrange
            IEntityWithImage entity = Fixture.Create<User>();
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
    }
}
