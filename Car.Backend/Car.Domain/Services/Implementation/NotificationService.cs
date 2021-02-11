using System.Collections.Generic;
using System.Linq;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Services.Interfaces;

namespace Car.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork<Notification> unitOfWork;

        public NotificationService(IUnitOfWork<Notification> unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public Notification GetNotification(int id)
        {
            return unitOfWork.GetRepository().Query(u => u.Sender).FirstOrDefault(u => u.Id == id);
        }

        public List<Notification> GetNotifications(int userId)
        {
            return unitOfWork.GetRepository().Query(m => m.Sender).Where(p => p.ReceiverId == userId).OrderByDescending(k => k.CreatedAt).ToList();
        }

        public int GetUnreadNotificationsNumber(int userId)
        {
            return unitOfWork.GetRepository().Query().Count(p => p.ReceiverId == userId && !p.IsRead);
        }

        public Notification UpdateNotification(Notification notification)
        {
            unitOfWork.GetRepository().Update(notification);
            unitOfWork.SaveChanges();
            return notification;
        }

        public Notification AddNotification(Notification notification)
        {
            unitOfWork.GetRepository().Add(notification);
            unitOfWork.SaveChanges();
            return notification;
        }

        public Notification DeleteNotification(int id)
        {
            var notification = unitOfWork.GetRepository().GetById(id);
            if (notification != null)
            {
                unitOfWork.GetRepository().Delete(notification);
                unitOfWork.SaveChanges();
            }

            return notification;
        }
    }
}
