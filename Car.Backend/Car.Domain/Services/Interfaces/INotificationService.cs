using System.Collections.Generic;
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
    }
}
