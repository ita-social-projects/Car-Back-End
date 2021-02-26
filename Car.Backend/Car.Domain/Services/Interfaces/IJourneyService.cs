using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Domain.Models;
using Car.Domain.Models.Journey;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        Task<List<JourneyModel>> GetPastJourneysAsync(int userId);

        Task<List<JourneyModel>> GetUpcomingJourneysAsync(int userId);

        Task<List<JourneyModel>> GetScheduledJourneysAsync(int userId);

        Task<JourneyModel> GetJourneyByIdAsync(int journeyId);
    }
}
