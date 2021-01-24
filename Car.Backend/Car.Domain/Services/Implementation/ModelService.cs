using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class ModelService : IModelService
    {
        private readonly IUnitOfWork<Model> unitOfWork;

        public ModelService(IUnitOfWork<Model> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<Model> GetModelsByBrandId(int brandId)
        {
            return unitOfWork.GetRepository()
                .Query()
                .Where(model => model.BrandId == brandId);
        }
    }
}
