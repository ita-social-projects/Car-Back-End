using Car.BLL.Dto;
using System.Collections.Generic;

namespace Car.BLL.Services.Interfaces
{
    public interface IModelService
    {
        IEnumerable<Modell> GetModels();
    }
}
