using System.IO;

namespace Car.BLL.Services.Interfaces
{
    public interface ICompressor
    {
        Stream CompressFile(Stream fileStream, int imageQuality);
    }
}
