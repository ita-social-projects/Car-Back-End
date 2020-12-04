using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface IDriveService<TFile>
    {
        Task<TFile> UploadFile(Stream fileStream, string folderId, string fileName, string contentType);

        Task DeleteFile(string fileId);

        Task<IEnumerable<TFile>> GetAllFiles();

        Task<TFile> GetFileById(string fileId);

        Task<byte[]> GetFileBytesById(string fileId);

        void SetCredentials(string credentialFilePath);
    }
}
