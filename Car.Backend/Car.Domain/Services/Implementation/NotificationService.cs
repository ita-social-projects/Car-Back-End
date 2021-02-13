using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork<Notification> notificationUnitOfWork;

        public NotificationService(IUnitOfWork<Notification> notificationUnitOfWork)
        {
            this.notificationUnitOfWork = notificationUnitOfWork;
        }

        public Notification GetNotification(int id)
        {
            return notificationUnitOfWork.GetRepository().Query(u => u.Sender).FirstOrDefault(u => u.Id == id);
        }

        public List<Notification> GetNotifications(int userId)
        {
            return notificationUnitOfWork.GetRepository().Query(m => m.Sender).Where(p => p.ReceiverId == userId).OrderByDescending(k => k.CreatedAt).ToList();
        }

        public int GetUnreadNotificationsNumber(int userId)
        {
            return notificationUnitOfWork.GetRepository().Query().Count(p => p.ReceiverId == userId && !p.IsRead);
        }

        public Notification UpdateNotification(Notification notification)
        {
            notificationUnitOfWork.GetRepository().Update(notification);
            notificationUnitOfWork.SaveChanges();
            return notification;
        }

        public Notification AddNotification(Notification notification)
        {
            notificationUnitOfWork.GetRepository().Add(notification);
            notificationUnitOfWork.SaveChanges();
            return notification;
        }

        public Notification DeleteNotification(int id)
        {
            var notification = notificationUnitOfWork.GetRepository().GetById(id);
            if (notification != null)
            {
                notificationUnitOfWork.GetRepository().Delete(notification);
                notificationUnitOfWork.SaveChanges();
            }

            return notification;
        }

        public async Task<Notification> GetNotificationAsync(int notificationId) =>
            await notificationUnitOfWork.GetRepository().Query()
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);

        public async Task<List<Notification>> GetNotificationsAsync(int userId)
        {
            var c = notificationUnitOfWork.GetRepository().Query();
            c.Include(notification => notification);
            c.Where(notificationReceiverId => notificationReceiverId.ReceiverId == userId).ToList();
            return await new Task<List<Notification>>(() => c.ToList());
        }

        public async Task<Notification> MarkAsReadAsync(int notificationId)
        {
            var result = await notificationUnitOfWork.GetRepository().Query()
                .FirstAsync(notification => notification.Id == notificationId);
            result.IsRead = true;
            await notificationUnitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
