using System;
using System.Threading.Tasks;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Car.DAL.Interfaces;

namespace Car.BLL.Services.Implementation
{
    public class JourneyService : IJourneyService
    {
        private IUnitOfWork<Journey> unitOfWork;

        public JourneyService(IUnitOfWork<Journey> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Journey GetJourneyById(int id)
        {
            return unitOfWork.GetRepository().GetById(id);
        }
    }
}
