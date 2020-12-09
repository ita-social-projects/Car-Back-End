using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using Car.BLL.Dto;
using Microsoft.AspNetCore.Http;
using System;
using Car.BLL.Exceptions;
using System.Threading.Tasks;
using System.Net;
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
            var entity = unitOfWork.GetRepository().GetById(entityId);

            if (entity == null)
            {
                throw new ApplicationCustomException($"This entity id - {entityId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            var result = await driveService.DeleteFile(entity.ImageId);

            if (result != string.Empty)
            {
                throw new ApplicationCustomException($"The image wasn't deleted")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Severity = Severity.Error,
                };
            }

            entity.ImageId = null;

            var newEntity = unitOfWork.GetRepository().Update(entity);
            unitOfWork.SaveChanges();

            return newEntity;
        }

        public async Task<string> GetImageBytesById(int entityId)
        {
            var entity = unitOfWork.GetRepository().GetById(entityId);

            if (entity == null)
            {
                throw new ApplicationCustomException($"This entity id - {entityId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            byte[] buffer = await driveService.GetFileBytesById(entity.ImageId);

            return Convert.ToBase64String(buffer);
        }

        public async Task<TEntity> UploadImage(int entityId, IFormFile entityFile)
        {
            if (entityFile == null)
            {
                throw new ApplicationCustomException("Received file is null")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Severity = Severity.Warning,
                };
            }

            var entity = unitOfWork.GetRepository().GetById(entityId);

            if (entity == null)
            {
                throw new ApplicationCustomException($"This entity id - {entityId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            var newFile = await driveService.UploadFile(
                 entityFile.OpenReadStream(),
                 strategy.GetFolderId(),
                 strategy.GetFileName(entity),
                 "image/png");

            if (newFile == null || newFile.Id == null)
            {
                throw new ApplicationCustomException("This image wasn't uploaded")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Severity = Severity.Error,
                };
            }

            entity.ImageId = newFile.Id;

            var newEntity = unitOfWork.GetRepository().Update(entity);

            unitOfWork.SaveChanges();
            return newEntity;
        }
    }
}
