using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notification> GetNotificationAsync(int notificationId);

        Task<List<Notification>> GetNotificationsAsync(int userId);

        Task<int> GetUnreadNotificationsNumberAsync(int userId);

        Task<Notification> UpdateNotificationAsync(Notification notification);

        Task<Notification> AddNotificationAsync(Notification notification);

        Task DeleteAsync(int notificationId);

        Task<Notification> CreateNewNotificationAsync(CreateNotificationDto createNotificationDto);

        Task<Notification> MarkNotificationAsReadAsync(int notificationId);

        Task JourneyUpdateNotifyUserAsync(Journey journey);

        Task NotifyParticipantsAboutCancellationAsync(Journey journey);

        Task DeleteNotificationsAsync(IEnumerable<Notification> notifications);

        Task NotifyDriverAboutParticipantWithdrawal(Journey journey, int participantId);
    }
}
