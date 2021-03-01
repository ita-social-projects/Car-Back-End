using System.Threading.Tasks;
using Car.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Services.Interfaces
{
    public interface IImageService
    {
        Task UploadImageAsync(IEntityWithImage entity, IFormFile entityFile);

        Task UpdateImageAsync(IEntityWithImage entity, IFormFile entityFile);

        Task DeleteImageAsync(IEntityWithImage entity);

        void SetImageLink(IEntityWithImage entity);
    }
}
