using Car.BLL.Services.Interfaces;
using Car.DAL.Interfaces;

namespace Car.BLL.Services.Implementation
{
    public class CarService : ICarService
    {
        private readonly IUnitOfWork<DAL.Entities.Car> unitOfWork;

        public CarService(
            IUnitOfWork<DAL.Entities.Car> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public DAL.Entities.Car GetCarById(int carId)
        {
            return unitOfWork.GetRepository().GetById(carId);
        }
    }
}
