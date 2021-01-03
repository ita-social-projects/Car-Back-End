using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Car.BLL.Dto;

namespace Car.BLL.Services.Implementation
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork<Brand> unitOfWork;

        public BrandService(IUnitOfWork<Brand> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            return unitOfWork.GetRepository().Query();
        }

        public IEnumerable<BrandModels> GetBrands()
        {
            return unitOfWork.GetRepository()
                .Query(brand => brand.Models)
                .Select(brand => new BrandModels
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Models = brand.Models.Select(model =>
                    new Modell
                    {
                        Id = model.Id,
                        Name = model.Name,
                        BrandName = model.Brand.Name,
                        BrandId = model.Brand.Id,
                    }),
                });
        }
    }
}
