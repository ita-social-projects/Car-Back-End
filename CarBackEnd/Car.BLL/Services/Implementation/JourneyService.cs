using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Car.BLL.Dto;

namespace Car.BLL.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private readonly IUnitOfWork<Journey> unitOfWork;

        public JourneyService(IUnitOfWork<Journey> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Journey GetJourneyById(int id)
        {
            return unitOfWork.GetRepository().GetById(id);
        }

        public IEnumerable<Journey> GetAllJourney()
        {
            return unitOfWork.GetRepository().Query().ToList();
        }


        public Journey AddJourney(Journey journey)
        {
            if (journey == null)
            {
                throw new Exceptions.DefaultApplicationException("Received data is null")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Severity = Severity.Error,
                };
            }

            var newEntity = unitOfWork.GetRepository().Add(journey);
            unitOfWork.SaveChanges();

            return newEntity;
        }

        public void RemoveJourney(int entityId)
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

            unitOfWork.GetRepository().Delete(entity);
            unitOfWork.SaveChanges();
        }

        public Journey UpdateJourney(Journey journey)
        {
            if (journey == null)
            {
                throw new Exceptions.DefaultApplicationException("Received data is null")
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Severity = Severity.Error,
                };
            }

            var newEntity = unitOfWork.GetRepository().Update(journey);
            unitOfWork.SaveChanges();

            return newEntity;
        }
    }
}
