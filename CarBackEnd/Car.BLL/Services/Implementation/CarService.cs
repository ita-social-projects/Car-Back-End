using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Car.BLL.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<DAL.Entities.Car> unitOfWork;
        private readonly IConfiguration configuration;

        public CarService(
            IUnitOfWork<DAL.Entities.Car> unitOfWork,
            IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        public DAL.Entities.Car GetCarById(int carId)
        {
            return unitOfWork.GetRepository().GetById(carId);
        }
    }
}
