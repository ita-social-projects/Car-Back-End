using System.IO;

namespace Car.Domain.Services.Interfaces
{
    public interface ICompressor
    {
        Stream CompressFile(Stream fileStream, int imageQuality);
    }
}
