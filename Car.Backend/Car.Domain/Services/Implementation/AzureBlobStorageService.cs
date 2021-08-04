using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Car.Domain.Configurations;
using Car.Domain.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Car.Domain.Services.Implementation
{
    public class AzureBlobStorageService : IFileService
    {
        private const int Quality = 40;

        private readonly IOptions<AzureBlobStorageOptions> azureBlobStorageOptions;
        private readonly ICompressor compressor;
        private readonly BlobServiceClient blobServiceClient;

        public AzureBlobStorageService(
            IOptions<AzureBlobStorageOptions> azureBlobStorageOptions,
            ICompressor compressor,
            BlobServiceClient blobServiceClient)
        {
            this.azureBlobStorageOptions = azureBlobStorageOptions;
            this.compressor = compressor;
            this.blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// Uploads a file from the specified stream.
        /// </summary>
        /// <param name="fileStream">Stream of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Identifier of the uploaded file.</returns>
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName)
        {
            var id = Guid.NewGuid().ToString();
            var fileMetadata = new Dictionary<string, string>
                { { "Name", fileName }, { "Parents", azureBlobStorageOptions.Value.Container! } };

            var blobContainerClient = blobServiceClient!.GetBlobContainerClient(azureBlobStorageOptions.Value.Container!);

            await using (fileStream)
            {
                try
                {
                    await using var compressedFile = compressor.CompressFile(fileStream, Quality);
                    id += ".jpg";
                    var blobClient = blobContainerClient.GetBlobClient(id);
                    await blobClient.UploadAsync(compressedFile, true);

                    await blobClient.SetMetadataAsync(fileMetadata);
                }
                catch (ArgumentException)
                {
                    id += Path.GetExtension(fileName);
                    var blobClient = blobContainerClient.GetBlobClient(id);
                    await blobClient.UploadAsync(fileStream, true);

                    await blobClient.SetMetadataAsync(fileMetadata);
                }
            }

            return id;
        }

        /// <summary>
        /// Deletes a file with the specified id.
        /// </summary>
        /// <param name="fileId">Identifier of the required file.</param>
        /// <returns>Result object.</returns>
        public async Task<bool> DeleteFileAsync(string fileId)
        {
            var blobContainerClient = blobServiceClient!.GetBlobContainerClient(azureBlobStorageOptions.Value.Container);
            var response = await blobContainerClient.DeleteBlobIfExistsAsync(fileId, DeleteSnapshotsOption.IncludeSnapshots);
            return response.Value;
        }
    }
}
