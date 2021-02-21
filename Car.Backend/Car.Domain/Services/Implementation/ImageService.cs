using System;
using System.Net;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Google.Apis.Drive.v3.Data;
using Microsoft.AspNetCore.Http;

namespace Car.Domain.Services.Implementation
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
                throw new Exceptions.DefaultApplicationException($"This entity id - {entityId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            var result = await driveService.DeleteFile(entity.ImageId);

            if (result != string.Empty)
            {
                throw new Exceptions.DefaultApplicationException($"The image wasn't deleted")
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
                throw new Exceptions.DefaultApplicationException($"This entity id - {entityId} wasn't found")
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Severity = Severity.Error,
                };
            }

            if (entity.ImageId == null)
            {
                return null;
            }

            var buffer = await driveService.GetFileBytesById(entity.ImageId);

            return Convert.ToBase64String(buffer);
        }

        public async Task<TEntity> UploadImage(int entityId, IFormFile entityFile)
        {
            if (entityFile == null)
            {
                throw new Exceptions.DefaultApplicationException("Received file is null")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Severity = Severity.Warning,
                };
            }

            var entity = unitOfWork.GetRepository().GetById(entityId);

            if (entity == null)
            {
                throw new Exceptions.DefaultApplicationException($"This entity id - {entityId} wasn't found")
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

            if (newFile == null)
            {
                throw new Exceptions.DefaultApplicationException("This image wasn't uploaded")
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
