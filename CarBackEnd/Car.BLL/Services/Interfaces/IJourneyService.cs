using System.Collections.Generic;
using Car.DAL.Entities;

namespace Car.BLL.Services.Interfaces
{
    public interface IJourneyService
    {
        Journey GetJourneyById(int id);

        IEnumerable<Journey> GetAllJourney();

        Journey AddJourney(Journey journey);

        void RemoveJourney(int id);

        Journey UpdateJourney(Journey journey);
    }
}
