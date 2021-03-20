using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Car.Domain.Configurations;
using Car.Domain.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.Domain.Services.Implementation
{
    public class GoogleDriveService : IFileService<File>
    {
        private const int Quality = 40;
        private const string IdFieldName = "id";

        private readonly ICompressor compressor;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IOptions<GoogleDriveOptions> googleDriveOptions;

        private DriveService service;

        public string RootFolderId => googleDriveOptions.Value.RootFolder;

        public GoogleDriveService(
            IOptions<GoogleDriveOptions> googleDriveOptions,
            IWebHostEnvironment webHostEnvironment,
            ICompressor compressor)
        {
            this.googleDriveOptions = googleDriveOptions;
            this.webHostEnvironment = webHostEnvironment;
            this.compressor = compressor;
            SetCredentials();
        }

        /// <summary>
        /// Sets the credentials for the google-drive service.
        /// </summary>
        public void SetCredentials()
        {
            var keyFilePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                googleDriveOptions.Value.CredentialsPath);

            var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream);
            credential = credential.CreateScoped(DriveService.Scope.Drive);

            service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = googleDriveOptions.Value.ApplicationName,
            });
        }

        /// <summary>
        /// Uploads a file from the specified stream.
        /// </summary>
        /// <param name="fileStream">Stream of the file.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the file content.</param>
        /// <returns>Identifier of the uploaded file.</returns>
        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var fileMetadata = new File { Name = fileName, Parents = new List<string> { RootFolderId } };

            FilesResource.CreateMediaUpload request;

            await using (fileStream)
            {
                await using var compressedFile = compressor.CompressFile(fileStream, Quality);
                request = service.Files.Create(fileMetadata, compressedFile, contentType);
                request.Fields = IdFieldName;
                await request.UploadAsync();
            }

            return request.ResponseBody.Id;
        }

        /// <summary>
        /// Deletes a file with the specified id.
        /// </summary>
        /// <param name="fileId">Identifier of the required file.</param>
        /// <returns>Result object.</returns>
        public Task<string> DeleteFileAsync(string fileId) =>
            service.Files.Delete(fileId).ExecuteAsync();
    }
}
