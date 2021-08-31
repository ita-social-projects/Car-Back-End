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
        Task<IEnumerable<JourneyModel>> GetPastJourneysAsync();

        Task<IEnumerable<JourneyModel>> GetUpcomingJourneysAsync();

        Task<IEnumerable<JourneyModel>> GetScheduledJourneysAsync();

        Task<JourneyModel> GetJourneyByIdAsync(int journeyId, bool withCancelledStops = false);

        Task<List<IEnumerable<StopDto>>> GetStopsFromRecentJourneysAsync(int countToTake = 5);

        Task AddFutureJourneyAsync();

        Task DeletePastJourneyAsync();

        Task<JourneyModel> AddJourneyAsync(JourneyDto journeyModel, int? parentId = null);

        IEnumerable<Journey> GetFilteredJourneys(JourneyFilter filter);

        Task<bool> DeleteAsync(int journeyId);

        Task<JourneyModel> UpdateRouteAsync(JourneyDto journeyDto, bool isParentUpdated = false);

        Task<JourneyModel> UpdateDetailsAsync(JourneyDto journeyDto, bool isParentUpdated = false);

        IEnumerable<ApplicantJourney> GetApplicantJourneys(JourneyFilter filter);

        Task CheckForSuitableRequests(Journey journey);

        Task CancelAsync(int journeyId);

        Task<bool> IsCanceled(int journeyId);

        Task<bool> DeleteUserFromJourney(int journeyId, int userId);

        Task<int> SetUnreadMessagesForNewUser(int journeyId);

        Task<bool> AddUserToJourney(JourneyApplyModel journeyApply);

        Task<(JourneyModel Journey, JourneyUserDto JourneyUser)> GetJourneyWithJourneyUserByIdAsync(
            int journeyId,
            int userId,
            bool withCancelledStops = false);
    }
}
