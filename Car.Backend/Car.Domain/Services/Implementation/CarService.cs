using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<Data.Entities.Car> unitOfWork;

        public CarService(
            IUnitOfWork<Data.Entities.Car> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Data.Entities.Car GetCarById(int carId)
        {
            return unitOfWork.GetRepository().GetById(carId);
        }
    }
}
