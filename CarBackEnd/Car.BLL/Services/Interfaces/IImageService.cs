using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface IImageService<TEntity, TFile>
    {
        Task<TEntity> UploadImage(int entityiD, IFormFile entityFile);

        Task<TEntity> DeleteImage(int entityiD);

        Task<string> GetImageBytesById(int entityId);
    }
}
