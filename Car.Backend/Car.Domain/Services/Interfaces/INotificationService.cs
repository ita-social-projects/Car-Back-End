using System.Collections.Generic;
using System.Threading.Tasks;
using Car.Data.Entities;

namespace Car.Domain.Services.Interfaces
{
    public interface INotificationService
    {
        Notification GetNotification(int id);

        List<Notification> GetNotifications(int userId);

        int GetUnreadNotificationsNumber(int userId);

        Notification UpdateNotification(Notification notification);

        Notification AddNotification(Notification notification);

        Notification DeleteNotification(int id);

        Task<Notification> GetNotificationAsync(int notificationId);

        Task<List<Notification>> GetNotificationsAsync(int userId);

        Task<Notification> MarkAsReadAsync(int notificationId);
    }
}
