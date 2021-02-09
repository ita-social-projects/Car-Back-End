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
            return unitOfWork.GetRepository().Query(u => u.User).Where(u => u.Id == id).FirstOrDefault();
        }

        public List<Notification> GetNotifications(int receiverId)
        {
            return unitOfWork.GetRepository().Query(m => m.User).Where(p => p.ReceiverId == receiverId).OrderByDescending(k => k.CreateAt).ToList();
        }

        public int GetUnreadNotificationsNumber(int userId)
        {
            return unitOfWork.GetRepository().Query().Where(p => p.ReceiverId == userId && p.IsRead == false).ToList().Count();
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

        public Notification DeleteNotification(int notificationId)
        {
            var notification = unitOfWork.GetRepository().GetById(notificationId);
            if (notification != null)
            {
                unitOfWork.GetRepository().Delete(notification);
                unitOfWork.SaveChanges();
            }

            return notification;
        }
    }
}
