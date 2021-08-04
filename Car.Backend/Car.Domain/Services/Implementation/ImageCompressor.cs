using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Car.Domain.Services.Interfaces;
using SkiaSharp;

namespace Car.Domain.Services.Implementation
{
    public class ImageCompressor : ICompressor
    {
        /// <summary>
        /// Compresses the file.
        /// </summary>
        /// <param name="fileStream">The file stream.</param>
        /// <param name="imageQuality">The image quality.</param>
        /// <returns>Stream of compressed file</returns>
        public Stream CompressFile(Stream fileStream, int imageQuality)
        {

            using var image = SKImage.FromEncodedData(fileStream);

            SKData data = image.Encode(SKEncodedImageFormat.Jpeg, imageQuality);

            return data.AsStream();
        }
    }
}
