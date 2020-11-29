using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Car.BLL.Services.Implementation
{
    class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImage(IFormFile img, string folderPath)
        {
            if (img == null)
            {
                return null;
            }

            string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid() + "_" + img.FileName;
            string newImagePath = Path.Combine(uploadFolder, uniqueFileName);
            string dbPathFile = Path.Combine(folderPath, uniqueFileName);
            using (var fileStream = new FileStream(newImagePath, FileMode.Create))
            {
                await img.CopyToAsync(fileStream);
            }

            return dbPathFile;
        }

        public void DeleteImage(string imagePath)
        {
            string deletePath = Path.Combine(_webHostEnvironment.WebRootPath, imagePath);
            var file = new FileInfo(deletePath);
            if (file.Exists)
            {
                file.Delete();
            }
        }
    }
}
