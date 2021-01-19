namespace Car.Domain.Services.Interfaces
{
    public interface ICarService
    {
        Data.Entities.Car GetCarById(int carId);
    }
}
