using System.IO;
using System.Threading.Tasks;

namespace Car.Domain.Services.Interfaces
{
    public interface IFileService<TFile>
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);

        Task<string> DeleteFileAsync(string fileId);

        void SetCredentials();
    }
}
