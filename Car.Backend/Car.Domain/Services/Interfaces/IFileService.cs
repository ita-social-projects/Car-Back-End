using System.IO;
using System.Threading.Tasks;

namespace Car.Domain.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName);

        Task<bool> DeleteFileAsync(string fileId);

        void SetCredentials();
    }
}
