using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Interfaces;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork<Notification> notificationUnitOfWork;

        public NotificationService(IUnitOfWork<Notification> notificationUnitOfWork) =>
            this.notificationUnitOfWork = notificationUnitOfWork;

        public async Task<Notification> GetNotificationAsync(int notificationId) =>
            await notificationUnitOfWork.GetRepository().Query(
                    notificationSender => notificationSender.Sender)
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);

        public async Task<List<Notification>> GetNotificationsAsync(int userId) =>
            await notificationUnitOfWork.GetRepository().Query(
                    m => m.Sender)
                .Where(p => p.ReceiverId == userId)
                .OrderByDescending(k => k.CreatedAt)
                .ToListAsync();

        public async Task<int> GetUnreadNotificationsNumberAsync(int userId) =>
            await notificationUnitOfWork.GetRepository().Query()
                .CountAsync(p => p.ReceiverId == userId && !p.IsRead);

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            var updatedNotification = await notificationUnitOfWork.GetRepository().UpdateAsync(notification);
            await notificationUnitOfWork.SaveChangesAsync();
            return updatedNotification;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            var addedNotification = await notificationUnitOfWork.GetRepository().AddAsync(notification);
            await notificationUnitOfWork.SaveChangesAsync();
            return addedNotification;
        }

        public async Task<Notification> DeleteNotificationAsync(int notificationId)
        {
            var result = await notificationUnitOfWork.GetRepository().Query()
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);
            await notificationUnitOfWork.GetRepository().DeleteAsync(result);
            await notificationUnitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            var result = await notificationUnitOfWork.GetRepository().Query()
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);
            result.IsRead = true;
            await notificationUnitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<Notification> CreateNewNotificationFromDtoAsync(NotificationDto notificationDto) =>
            await Task.Run(() => new Notification
            {
                SenderId = notificationDto.SenderId,
                ReceiverId = notificationDto.ReceiverId,
                Type = notificationDto.Type,
                JsonData = notificationDto.JsonData,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
            });
    }
}