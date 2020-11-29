using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    interface IImageService
    {
        Task<string> UploadImage(IFormFile image, string folderPath);

        void DeleteImage(string imagePath);
    }
}
