using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface IModelService
    {
        Task<List<Model>> GetModelsByBrandIdAsync(int brandId);
    }
}
