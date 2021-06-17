using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Filters;
using Car.Domain.Models.Journey;

namespace Car.Domain.Services.Interfaces
{
    public interface IJourneyService
    {
        Task<IEnumerable<JourneyModel>> GetPastJourneysAsync(int userId);

        Task<IEnumerable<JourneyModel>> GetUpcomingJourneysAsync(int userId);

        Task<IEnumerable<JourneyModel>> GetScheduledJourneysAsync(int userId);

        Task<JourneyModel> GetJourneyByIdAsync(int journeyId);

        Task<List<IEnumerable<StopDto>>> GetStopsFromRecentJourneysAsync(int userId, int countToTake = 5);

        Task DeletePastJourneyAsync();

        Task<JourneyModel> AddJourneyAsync(JourneyDto journeyModel);

        Task<IEnumerable<Journey>> GetFilteredJourneys(JourneyFilter filter);

        Task DeleteAsync(int journeyId);

        Task<JourneyModel> UpdateRouteAsync(JourneyDto journeyDto);

        Task<JourneyModel> UpdateDetailsAsync(JourneyDto journeyDto);

        Task<IEnumerable<ApplicantJourney>> GetApplicantJourneys(JourneyFilter filter);

        Task CheckForSuitableRequests(Journey journey);

        Task CancelAsync(int journeyId);

        Task<bool> IsCanceled(int journeyId);

        Task DeleteUserFromJourney(int journeyId, int userId);
    }
}
