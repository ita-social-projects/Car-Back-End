using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Extensions
{
    public static class CarServiceExtension
    {
        public static IQueryable<Data.Entities.Car> IncludeModelWithBrand(this IQueryable<Data.Entities.Car> cars) =>
            cars.Include(car => car.Model).ThenInclude(model => model!.Brand);
    }
}
