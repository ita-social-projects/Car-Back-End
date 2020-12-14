using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Car.BLL.Services.Interfaces;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.BLL.Services.Implementation
{
    public class GoogleDriveService : IDriveService<File>
    {
        private readonly ICompressor compressor;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        private DriveService service;

        public GoogleDriveService(
            IConfiguration configuration,
            IWebHostEnvironment webHostEnvironment,
            ICompressor compressor)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
            this.compressor = compressor;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <returns>Empty string if successful</returns>
        public Task<string> DeleteFile(string fileId)
        {
            return service.Files.Delete(fileId).ExecuteAsync();
        }

        /// <summary>
        /// Gets all files.
        /// </summary>
        /// <returns>All files</returns>
        public async Task<IEnumerable<File>> GetAllFiles()
        {
            return (await service.Files.List().ExecuteAsync()).Files;
        }

        /// <summary>
        /// Gets the file by identifier.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <returns>The file instance</returns>
        public Task<File> GetFileById(string fileId)
        {
            return service.Files.Get(fileId).ExecuteAsync();
        }

        /// <summary>
        /// Gets the file bytes by identifier.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <returns>base64 array of file</returns>
        public async Task<byte[]> GetFileBytesById(string fileId)
        {
            using var stream = new MemoryStream();
            await service.Files.Get(fileId).DownloadAsync(stream);
            return stream.GetBuffer();
        }

        /// <summary>
        /// Sets the credentials.
        /// </summary>
        /// <param name="credentialFilePath">The credential file path.</param>
        public void SetCredentials(string credentialFilePath)
        {
            var keyFilePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                "Credentials",
                credentialFilePath);

            var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream);
            credential = credential.CreateScoped(new string[] { DriveService.Scope.Drive });

            service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = configuration["GoogleApplicationName"],
            });
        }

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="folderId">The folder identifier.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="contentType">Type of the content.</param>
        /// <returns>Uploaded file</returns>
        public async Task<File> UploadFile(Stream fileStream, string folderId, string fileName, string contentType)
        {
            const int quality = 40;

            var fileMetadata = new File();
            fileMetadata.Name = fileName;
            fileMetadata.Parents = new List<string> { folderId };

            FilesResource.CreateMediaUpload request;

            using (fileStream)
            {
                using var compresedFile = compressor.CompressFile(fileStream, quality);
                request = service.Files.Create(fileMetadata, compresedFile, contentType);
                request.Fields = "id, name, webViewLink, size";
                await request.UploadAsync();
            }

            return request.ResponseBody;
        }
    }
}