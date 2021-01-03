using Car.BLL.Dto;
using Car.DAL.Entities;
using System.Collections.Generic;

namespace Car.BLL.Services.Interfaces
{
    public interface IBrandService
    {
        IEnumerable<BrandModels> GetBrands();

        IEnumerable<Brand> GetAllBrands();
    }
}
