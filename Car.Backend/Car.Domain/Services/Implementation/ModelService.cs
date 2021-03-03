using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class ModelService : IModelService
    {
        private readonly IRepository<Model> modelRepository;

        public ModelService(IRepository<Model> modelRepository) =>
            this.modelRepository = modelRepository;

        public Task<List<Model>> GetModelsByBrandIdAsync(int brandId) =>
            modelRepository.Query().Where(model => model.BrandId == brandId).ToListAsync();
    }
}
