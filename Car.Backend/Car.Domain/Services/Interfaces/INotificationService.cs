using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Task<NotificationDto> GetNotificationAsync(int notificationId);

        Task<IEnumerable<Notification>> GetNotificationsAsync();

        Task<int> GetUnreadNotificationsNumberAsync();

        Task<NotificationDto> UpdateNotificationAsync(NotificationDto notification);

        Task<NotificationDto> AddNotificationAsync(NotificationDto notification);

        Task DeleteAsync(int notificationId);

        Task<NotificationDto> CreateNewNotificationAsync(CreateNotificationDto createNotificationDto);

        Task<NotificationDto> MarkNotificationAsReadAsync(int notificationId);

        Task JourneyUpdateNotifyUserAsync(Journey journey);

        Task NotifyParticipantsAboutCancellationAsync(Journey journey);

        Task DeleteNotificationsAsync(IEnumerable<Notification> notifications);

        Task NotifyDriverAboutParticipantWithdrawal(Journey journey, int participantId);
    }
}
