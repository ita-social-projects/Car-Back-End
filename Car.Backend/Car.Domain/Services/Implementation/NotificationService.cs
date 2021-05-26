using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Hubs;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> notificationRepository;
        private readonly IHubContext<SignalRHub> notificationHub;
        private readonly IMapper mapper;

        public NotificationService(
            IRepository<Notification> notificationRepository,
            IHubContext<SignalRHub> notificationHub,
            IMapper mapper)
        {
            this.notificationRepository = notificationRepository;
            this.notificationHub = notificationHub;
            this.mapper = mapper;
        }

        public Task<Notification> GetNotificationAsync(int notificationId) =>
            notificationRepository.Query(notificationSender => notificationSender.Sender)
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);

        public Task<List<Notification>> GetNotificationsAsync(int userId) =>
            notificationRepository.Query(m => m.Sender)
                .Where(p => p.ReceiverId == userId)
                .OrderByDescending(k => k.CreatedAt)
                .ToListAsync();

        public Task<int> GetUnreadNotificationsNumberAsync(int userId) =>
            notificationRepository.Query()
                .CountAsync(p => p.ReceiverId == userId && !p.IsRead);

        public async Task<Notification> UpdateNotificationAsync(Notification notification)
        {
            await notificationRepository.UpdateAsync(notification);
            await notificationRepository.SaveChangesAsync();

            return notification;
        }

        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            var addedNotification = await notificationRepository.AddAsync(notification);
            await notificationRepository.SaveChangesAsync();

            await NotifyClientAsync(notification);

            return addedNotification;
        }

        public async Task DeleteAsync(int notificationId)
        {
            notificationRepository.Delete(new Notification() { Id = notificationId });
            await notificationRepository.SaveChangesAsync();
        }

        public async Task<Notification> MarkNotificationAsReadAsync(int notificationId)
        {
            var notificationToUpdate = await notificationRepository.Query()
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);
            notificationToUpdate.IsRead = true;
            await notificationRepository.SaveChangesAsync();

            await NotifyClientAsync(notificationToUpdate);

            return notificationToUpdate;
        }

        public Task<Notification> CreateNewNotificationAsync(CreateNotificationModel createNotificationModel) =>
            Task.Run(() => mapper.Map<CreateNotificationModel, Notification>(createNotificationModel));

        private async Task NotifyClientAsync(Notification notification)
        {
            await notificationHub.Clients.All.SendAsync("sendToReact", notification);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await GetUnreadNotificationsNumberAsync(notification.ReceiverId));
        }
    }
}
