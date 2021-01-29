using System.Collections.Generic;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
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
    }
}
