using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Car.Domain.Services.Interfaces
{
    public interface IImageService<TEntity, TFile>
    {
        Task<TEntity> UploadImage(int entityId, IFormFile entityFile);

        Task<TEntity> DeleteImage(int entityId);

        Task<string> GetImageBytesById(int entityId);
    }
}
