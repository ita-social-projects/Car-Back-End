using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CarEntity = Car.DAL.Entities.Car;

namespace Car.BLL.Services.Interfaces
{
    public interface ICarService
    {
        CarEntity GetCarById(int carId);

        Task<CarEntity> UploadCarPhoto(int carId, IFormFile carFile);

        Task<CarEntity> DeleteCarAvatar(int carId, string carFileId);

        Task<string> GetCarFileBytesById(int carId);
    }
}
