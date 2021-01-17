using Car.BLL.Dto;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork<Model> unitOfWork;

        public ModelService(IUnitOfWork<Model> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Modell> GetModels()
        {
            return unitOfWork.GetRepository().Query(model => model.Brand)
                .Select(model => new Modell
                {
                    Id = model.Id,
                    Name = model.Name,
                    BrandId = model.Brand.Id,
                    BrandName = model.Brand.Name,
                });
        }

        public IEnumerable<Model> GetModelsByBrandId(int brandId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Where(model => model.BrandId == brandId);
        }
    }
}
