using System.Threading.Tasks;
using Car.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Services.Interfaces
{
    public interface IImageService
    {
        Task<IEntityWithImage> UploadImageAsync(IEntityWithImage entity, IFormFile entityFile);

        Task<IEntityWithImage> UpdateImageAsync(IEntityWithImage entity, IFormFile entityFile);

        Task<IEntityWithImage> DeleteImageAsync(IEntityWithImage entity);
    }
}
