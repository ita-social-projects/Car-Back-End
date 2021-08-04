using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Car.Domain.Services.Interfaces;

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
            using var image = Image.FromStream(fileStream);

            var imageQualityParameter = new EncoderParameter(
                Encoder.Quality, imageQuality);

            var imageCodecInfos = ImageCodecInfo.GetImageEncoders();

            var codecParameter = new EncoderParameters(1) { Param = { [0] = imageQualityParameter } };

            var jpegCodec = imageCodecInfos.FirstOrDefault(t => t.MimeType == "image/jpeg");

            var compressedFile = new MemoryStream();

            image.Save(compressedFile, jpegCodec!, codecParameter);

            compressedFile.Position = 0;

            return compressedFile;
        }
    }
}
