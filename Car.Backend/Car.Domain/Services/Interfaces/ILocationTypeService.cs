using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface ILocationTypeService
    {
        public Task<IEnumerable<LocationType>> GetAllLocationTypesAsync();
    }
}
