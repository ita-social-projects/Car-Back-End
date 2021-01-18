using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class CompressorWithQuality : ICompressor
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

            var imageQualitysParameter = new EncoderParameter(
                Encoder.Quality, imageQuality);

            var alleCodecs = ImageCodecInfo.GetImageEncoders();

            var codecParameter = new EncoderParameters(1);
            codecParameter.Param[0] = imageQualitysParameter;

            var jpegCodec = alleCodecs.FirstOrDefault(t => t.MimeType == "image/jpeg");

            var compressedFile = new MemoryStream();

            image.Save(compressedFile, jpegCodec, codecParameter);

            return compressedFile;
        }
    }
}
