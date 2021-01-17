using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Car.BLL.Services.Interfaces
{
    public interface IImageService<TEntity, TFile>
    {
        Task<TEntity> UploadImage(int entityId, IFormFile entityFile);

        Task<TEntity> DeleteImage(int entityId);

        Task<string> GetImageBytesById(int entityId);
    }
}
