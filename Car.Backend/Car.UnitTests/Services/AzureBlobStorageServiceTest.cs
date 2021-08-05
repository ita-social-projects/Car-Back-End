using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Car.Domain.Configurations;
using Car.Domain.Services.Implementation;
using Car.Domain.Services.Interfaces;
using Car.UnitTests.Base;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace Car.UnitTests.Services
{
    public class AzureBlobStorageServiceTest : TestBase
    {
        [Theory]
        [AutoEntityData]
        public async Task UploadFileAsync_FileStreamIsValid_ReturnsIdWithJpgExtension(
            Stream fileStream,
            string filename,
            [Frozen] Mock<ICompressor> compressor,
            [Frozen] Mock<BlobServiceClient> blobServiceClient,
            [Frozen] Mock<BlobContainerClient> blobContainerClient,
            [Frozen] Mock<BlobClient> blobClient,
            [Frozen] IOptions<AzureBlobStorageOptions> azureBlobStorageOptions,
            Stream fileStreamResult,
            Mock<Response<BlobContentInfo>> blobContentInfoResponse,
            Mock<Response<BlobInfo>> blobInfoResponse)
        {
            // Arrange
            compressor
                .Setup(c => c.CompressFile(It.IsAny<Stream>(), It.IsAny<int>()))
                .Returns(fileStreamResult);
            blobServiceClient
                .Setup(serviceClient => serviceClient.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(blobContainerClient.Object);
            blobContainerClient
                .Setup(containerClient => containerClient.GetBlobClient(It.IsAny<string>()))
                .Returns(blobClient.Object);
            blobClient
                .Setup(client =>
                    client.UploadAsync(
                        It.IsAny<Stream>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(blobContentInfoResponse.Object);
            blobClient
                .Setup(client =>
                    client.SetMetadataAsync(
                        It.IsAny<IDictionary<string, string>>(),
                        It.IsAny<BlobRequestConditions>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(blobInfoResponse.Object);
            AzureBlobStorageService azureBlobStorageService =
                new(azureBlobStorageOptions, compressor.Object, blobServiceClient.Object);

            // Act
            var result = await azureBlobStorageService.UploadFileAsync(fileStream, filename);

            // Assert
            result.Should().EndWith(".jpg");
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteFileAsync_FileIsExist_ReturnsTrue(
            string fileId,
            [Frozen] Mock<ICompressor> compressor,
            [Frozen] Mock<BlobServiceClient> blobServiceClient,
            [Frozen] Mock<BlobContainerClient> blobContainerClient,
            [Frozen] IOptions<AzureBlobStorageOptions> azureBlobStorageOptions,
            Mock<Response<bool>> boolResponse)
        {
            // Arrange
            boolResponse.SetupGet(bResponse => bResponse.Value).Returns(true);
            blobServiceClient
                .Setup(serviceClient => serviceClient.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(blobContainerClient.Object);
            blobContainerClient
                .Setup(containerClient => containerClient.DeleteBlobIfExistsAsync(
                    It.IsAny<string>(),
                    It.IsAny<DeleteSnapshotsOption>(),
                    It.IsAny<BlobRequestConditions>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(boolResponse.Object);
            AzureBlobStorageService azureBlobStorageService =
                new(azureBlobStorageOptions, compressor.Object, blobServiceClient.Object);

            // Act
            var result = await azureBlobStorageService.DeleteFileAsync(fileId);

            // Assert
            result.Should().BeTrue();
        }

        [Theory]
        [AutoEntityData]
        public async Task DeleteFileAsync_FileIsNotExist_ReturnsFalse(
            string fileId,
            [Frozen] Mock<ICompressor> compressor,
            [Frozen] Mock<BlobServiceClient> blobServiceClient,
            [Frozen] Mock<BlobContainerClient> blobContainerClient,
            [Frozen] IOptions<AzureBlobStorageOptions> azureBlobStorageOptions,
            Mock<Response<bool>> boolResponse)
        {
            // Arrange
            boolResponse.SetupGet(bResponse => bResponse.Value).Returns(false);
            blobServiceClient
                .Setup(serviceClient => serviceClient.GetBlobContainerClient(It.IsAny<string>()))
                .Returns(blobContainerClient.Object);
            blobContainerClient
                .Setup(containerClient => containerClient.DeleteBlobIfExistsAsync(
                    It.IsAny<string>(),
                    It.IsAny<DeleteSnapshotsOption>(),
                    It.IsAny<BlobRequestConditions>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(boolResponse.Object);
            AzureBlobStorageService azureBlobStorageService =
                new(azureBlobStorageOptions, compressor.Object, blobServiceClient.Object);

            // Act
            var result = await azureBlobStorageService.DeleteFileAsync(fileId);

            // Assert
            result.Should().BeFalse();
        }
    }
}
