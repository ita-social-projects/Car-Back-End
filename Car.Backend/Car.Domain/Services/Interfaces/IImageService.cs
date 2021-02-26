using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Services.Interfaces
{
    public interface IImageService<TEntity, TFile>
    {
        Task<TEntity> UploadImageAsync(int entityId, IFormFile entityFile);

        Task<TEntity> DeleteImageAsync(int entityId);

        Task<string> GetFileLinkAsync(string fileId);
    }
}
