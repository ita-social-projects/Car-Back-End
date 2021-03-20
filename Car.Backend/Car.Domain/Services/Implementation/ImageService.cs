using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Services.Implementation
{
    public class ImageService : IImageService
    {
        private const string ImageContentType = "image/png";
        private readonly IFileService<File> fileService;

        public ImageService(IFileService<File> fileService) =>
            this.fileService = fileService;

        /// <summary>
        /// Uploads a new image for an entity with image.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="entityFile">Image to upload.</param>
        /// <returns>Task.</returns>
        public async Task<IEntityWithImage> UploadImageAsync(IEntityWithImage entity, IFormFile entityFile)
        {
            if (entityFile != null && entity != null)
            {
                entity.ImageId = await fileService.UploadFileAsync(entityFile.OpenReadStream(), entityFile.FileName, ImageContentType);
            }

            return entity;
        }

        /// <summary>
        /// Updates an image for an entity with image.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="entityFile">Image to upload.</param>
        /// <returns>Task.</returns>
        public async Task<IEntityWithImage> UpdateImageAsync(IEntityWithImage entity, IFormFile entityFile)
        {
            await DeleteImageAsync(entity);
            await UploadImageAsync(entity, entityFile);

            return entity;
        }

        /// <summary>
        /// Deletes an image from file service using the image id from the input entity.
        /// </summary>
        /// <param name="entity">Entity with an image to delete.</param>
        /// <returns>Task.</returns>
        public async Task<IEntityWithImage> DeleteImageAsync(IEntityWithImage entity)
        {
            if (entity?.ImageId != null)
            {
                await fileService.DeleteFileAsync(entity.ImageId);
                entity.ImageId = null;
            }

            return entity;
        }
    }
}
