using Car.BLL.Services.Interfaces;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Car.BLL.Services.Implementation
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
            Image image = Image.FromStream(fileStream);

            ImageCodecInfo jpegCodec = null;

            EncoderParameter imageQualitysParameter = new EncoderParameter(
                Encoder.Quality, imageQuality);

            ImageCodecInfo[] alleCodecs = ImageCodecInfo.GetImageEncoders();

            EncoderParameters codecParameter = new EncoderParameters(1);
            codecParameter.Param[0] = imageQualitysParameter;

            for (int i = 0; i < alleCodecs.Length; i++)
            {
                if (alleCodecs[i].MimeType == "image/jpeg")
                {
                    jpegCodec = alleCodecs[i];
                    break;
                }
            }

            Stream compressedFile = new MemoryStream();

            image.Save(compressedFile, jpegCodec, codecParameter);

            return compressedFile;
        }
    }
}
