using System.Collections.Generic;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<Brand> GetAllBrands();
    }
}
