using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using CarEntity = Car.DAL.Entities.Car;

namespace Car.BLL.Services.Interfaces
{
    public interface ICarService
    {
        CarEntity GetCarById(int carId);
    }
}
