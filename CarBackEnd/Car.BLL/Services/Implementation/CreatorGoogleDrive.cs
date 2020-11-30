using Car.BLL.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation
{
    class CreatorGoogleDrive : ICreatorDrive<File>
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;
        private IDriveService<File> carDriveService;
        private IDriveService<File> avatarDriveService;

        public CreatorGoogleDrive(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IDriveService<File> CarDriveService =>
            carDriveService ??= new GoogleDriveService(
                configuration, webHostEnvironment, "credential-car-drive.json");

        public IDriveService<File> AvatarDriveService =>
            avatarDriveService ??= new GoogleDriveService(
                configuration, webHostEnvironment, "credential-avatar-drive.json");
    }
}
