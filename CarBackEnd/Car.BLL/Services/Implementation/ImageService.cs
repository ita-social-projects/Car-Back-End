using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v3.Data.File;

namespace Car.BLL.Services.Implementation
{
    public class ImageService<TEntity> : IImageService<TEntity, File>
        where TEntity : class, IEntityWithImage
    {
        private readonly IDriveService<File> driveService;
        private readonly IUnitOfWork<TEntity> unitOfWork;
        private readonly IEntityTypeStrategy<TEntity> strategy;

        public ImageService(
            IDriveService<File> driveService,
            IUnitOfWork<TEntity> unitOfWork,
            IEntityTypeStrategy<TEntity> strategy)
        {
            this.driveService = driveService;
            this.unitOfWork = unitOfWork;
            this.strategy = strategy;
            driveService.SetCredentials(strategy.GetCredentialFilePath());
        }

        public async Task<TEntity> DeleteImage(int entityId)
        {
            TEntity entity = unitOfWork.GetRepository().GetById(entityId);

            await driveService.DeleteFile(entity.ImageId);

            entity.ImageId = null;

            TEntity newEntity = unitOfWork.GetRepository().Update(entity);
            unitOfWork.SaveChanges();

            return newEntity;
        }

        public async Task<string> GetImageBytesById(int entityId)
        {
            TEntity entity = unitOfWork.GetRepository().GetById(entityId);
            byte[] buffer = await driveService.GetFileBytesById(entity.ImageId);

            return Convert.ToBase64String(buffer);
        }

        public async Task<TEntity> UploadImage(int entityId, IFormFile entityFile)
        {
            TEntity entity = unitOfWork.GetRepository().GetById(entityId);

            File newFile = await driveService.UploadFile(
                 entityFile.OpenReadStream(),
                 strategy.GetFolderId(),
                 strategy.GetFileName(entity),
                 "image/png");

            entity.ImageId = newFile.Id;

            TEntity newEntity = unitOfWork.GetRepository().Update(entity);

            unitOfWork.SaveChanges();
            return newEntity;
        }
    }
}
