using Car.BLL.Dto;
using Car.DAL.Entities;
using System.Collections.Generic;

namespace Car.BLL.Services.Interfaces
{
    public interface IModelService
    {
        IEnumerable<Modell> GetModels();

        IEnumerable<Model> GetModelsByBrandId(int brandId);
    }
}
