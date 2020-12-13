using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using System.Text;

namespace Car.BLL.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<DAL.Entities.Car> unitOfWork;
        private readonly IConfiguration configuration;
        private readonly IDriveService<File> userDriveCars;

        public CarService(
            IUnitOfWork<DAL.Entities.Car> unitOfWork,
            IConfiguration configuration,
            IDriveService<File> userDriveCars)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.userDriveCars = userDriveCars;
            userDriveCars.SetCredentials(configuration["CredentialsFile:CarDriveCredential"]);
        }

        public async Task<DAL.Entities.Car> DeleteCarAvatar(int carId, string carFileId)
        {
            DAL.Entities.Car car = unitOfWork.GetRepository().GetById(carId);

            await userDriveCars.DeleteFile(carFileId);

            car.ImageCar = null;

            DAL.Entities.Car newCar = unitOfWork.GetRepository().Update(car);
            unitOfWork.SaveChanges();
            return newCar;
        }

        public DAL.Entities.Car GetCarById(int carId)
        {
            return unitOfWork.GetRepository().GetById(carId);
        }

        public async Task<string> GetCarFileBytesById(int carId)
        {
            DAL.Entities.Car car = unitOfWork.GetRepository().GetById(carId);

            byte[] buffer = await userDriveCars.GetFileBytesById(car.ImageCar);

            return Convert.ToBase64String(buffer);
        }



        public async Task<DAL.Entities.Car> UploadCarPhoto(int carId, IFormFile carFile)
        {
            DAL.Entities.Car car = unitOfWork.GetRepository().GetById(carId);

            StringBuilder fileName = new StringBuilder();
            fileName.Append(car.Id).Append("_")
                .Append(car.Brand).Append("_")
                .Append(car.Model).Append(".jpg");

            File newFile = await userDriveCars.UploadFile(
                 carFile.OpenReadStream(),
                 configuration["GoogleFolders:UserCarFolder"],
                 fileName.ToString(),
                 "image/png");

            car.ImageCar = newFile.Id;

            DAL.Entities.Car newCar = unitOfWork.GetRepository().Update(car);
            unitOfWork.SaveChanges();
            return newCar;
        }
    }
}
