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

        Task<Notification> DeleteNotificationAsync(int notificationId);

        Task<Notification> CreateNewNotificationFromDtoAsync(NotificationDto notificationDto);

        Task<Notification> MarkNotificationAsReadAsync(int notificationId);
    }
}
