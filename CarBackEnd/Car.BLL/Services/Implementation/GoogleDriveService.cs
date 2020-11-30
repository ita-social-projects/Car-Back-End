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
    class GoogleDriveService : IDriveService<File>
    {
        private readonly DriveService service;

        public GoogleDriveService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment, string pathCredentialFile)
        {
            var keyFilePath = Path.Combine(
                webHostEnvironment.WebRootPath,
                "Credentials",
                pathCredentialFile);

            var stream = new FileStream(keyFilePath, FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream);
            credential = credential.CreateScoped(new string[] { DriveService.Scope.Drive });

            service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = configuration["GoogleApplicationName"],
            });
        }

        public async Task DeleteFile(string fileId)
        {
           await service.Files.Delete(fileId).ExecuteAsync();
        }

        public async Task<IEnumerable<File>> GetAllFiles()
        {
           return (await service.Files.List().ExecuteAsync()).Files;
        }

        public async Task<File> GetFileById(string fileId)
        {
            return await service.Files.Get(fileId).ExecuteAsync();
        }

        public async Task<File> UploadFile(Stream fileStream, string folderId, string fileName, string contentType)
        {
            File fileMetadata = new File();
            fileMetadata.Name = fileName;
            fileMetadata.Parents = new List<string> { folderId };

            FilesResource.CreateMediaUpload request;

            using (fileStream)
            {
                request = service.Files.Create(fileMetadata, fileStream, contentType);
                request.Fields = "id, name, webViewLink, size";
                await request.UploadAsync();
            }

            return request.ResponseBody;
        }
    }
}
