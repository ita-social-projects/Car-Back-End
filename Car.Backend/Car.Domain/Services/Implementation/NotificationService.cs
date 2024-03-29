﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;
using Car.Domain.Hubs;
using Car.Domain.Services.Interfaces;
using Car.WebApi.ServiceExtension;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Car.Domain.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification> notificationRepository;
        private readonly IRepository<User> userRepository;
        private readonly IHubContext<SignalRHub> notificationHub;
        private readonly IMapper mapper;
        private readonly IPushNotificationService pushNotificationService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IJourneyUserService journeyUserService;

        public NotificationService(
            IRepository<Notification> notificationRepository,
            IRepository<User> userRepository,
            IHubContext<SignalRHub> notificationHub,
            IPushNotificationService pushNotificationService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IJourneyUserService journeyUserService)
        {
            this.notificationRepository = notificationRepository;
            this.userRepository = userRepository;
            this.notificationHub = notificationHub;
            this.pushNotificationService = pushNotificationService;
            this.mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
            this.journeyUserService = journeyUserService;
        }

        public async Task<NotificationDto> GetNotificationAsync(int notificationId) =>
            mapper.Map<Notification, NotificationDto>(
              await notificationRepository.Query(notificationSender => notificationSender!.Sender!)
              .FirstOrDefaultAsync(notification => notification.Id == notificationId));

        public async Task<IEnumerable<Notification>> GetNotificationsAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

            return await notificationRepository.Query().Include(n => n.Sender)
                            .Where(p => p.ReceiverId == userId)
                            .OrderByDescending(k => k.CreatedAt)
                            .ToListAsync();
        }

        public Task<int> GetUnreadNotificationsNumberAsync()
        {
            int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
            return notificationRepository.Query()
                       .CountAsync(p => p.ReceiverId == userId && !p.IsRead);
        }

        public async Task<NotificationDto> UpdateNotificationAsync(NotificationDto notification)
        {
            var updateNotification = mapper.Map<NotificationDto, Notification>(notification);
            await notificationRepository.UpdateAsync(updateNotification);
            await notificationRepository.SaveChangesAsync();

            return notification;
        }

        public async Task<NotificationDto> AddNotificationAsync(NotificationDto notification)
        {
            var addNotification = mapper.Map<NotificationDto, Notification>(notification);
            await notificationRepository.AddAsync(addNotification);
            await notificationRepository.SaveChangesAsync();

            await NotifyClientAsync(notification);
            await pushNotificationService.SendNotificationAsync(notification);

            return notification;
        }

        public async Task<bool> DeleteAsync(int notificationId)
        {
            var notification = await notificationRepository.GetByIdAsync(notificationId);

            if (notification != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);
                if (userId != notification.ReceiverId)
                {
                    return false;
                }

                notificationRepository.Delete(notification);
                await notificationRepository.SaveChangesAsync();
                await NotifyDeleteClientAsync(notificationId);
            }

            return true;
        }

        public async Task<(bool IsUpdated, NotificationDto? UpdatedNotificationDto)> MarkNotificationAsReadAsync(int notificationId)
        {
            var notificationToUpdate = await notificationRepository.Query()
                .FirstOrDefaultAsync(notification => notification.Id == notificationId);

            if (notificationToUpdate != null)
            {
                int userId = httpContextAccessor.HttpContext!.User.GetCurrentUserId(userRepository);

                if (userId != notificationToUpdate.ReceiverId)
                {
                    return (false, null);
                }

                notificationToUpdate.IsRead = true;
                await notificationRepository.SaveChangesAsync();

                await NotifyClientAsync(mapper.Map<Notification, NotificationDto>(notificationToUpdate!));
            }

            return (true, mapper.Map<Notification, NotificationDto>(notificationToUpdate!));
        }

        public Task<NotificationDto> CreateNewNotificationAsync(CreateNotificationDto createNotificationDto) =>
            Task.Run(() => mapper.Map<CreateNotificationDto, NotificationDto>(createNotificationDto));

        public async Task JourneyUpdateNotifyUserAsync(Journey journey)
        {
            if (journey?.Participants is null)
            {
                return;
            }

            foreach (var participant in journey.Participants)
            {
                await AddNotificationAsync(new NotificationDto()
                {
                    SenderId = journey.Organizer!.Id,
                    ReceiverId = participant.Id,
                    Type = NotificationType.JourneyDetailsUpdate,
                    IsRead = false,
                    JourneyId = journey.Id,
                    JsonData = JsonSerializer.Serialize(new { }),
                });
            }
        }

        public async Task NotifyParticipantsAboutCancellationAsync(Journey journey)
        {
            if (journey?.Participants is null)
            {
                return;
            }

            foreach (var user in journey.Participants)
            {
                await AddNotificationAsync(new NotificationDto()
                {
                    SenderId = journey.Organizer!.Id,
                    ReceiverId = user.Id,
                    Type = NotificationType.JourneyCancellation,
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,
                    JourneyId = journey.Id,
                    JsonData = JsonSerializer.Serialize(new { }),
                });
            }
        }

        public async Task DeleteNotificationsAsync(IEnumerable<Notification> notifications)
        {
            await notificationRepository.DeleteRangeAsync(notifications);
            await notificationRepository.SaveChangesAsync();
        }

        public async Task NotifyDriverAboutParticipantWithdrawal(Journey journey, int participantId)
        {
            var journeyUser = await journeyUserService.GetJourneyUserByIdAsync(journey.Id, participantId);
            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            };

            await AddNotificationAsync(new NotificationDto()
            {
                SenderId = participantId,
                ReceiverId = journey.Organizer!.Id,
                Type = NotificationType.PassengerWithdrawal,
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                JourneyId = journey.Id,
                JsonData = JsonSerializer.Serialize(new { JourneyUser = journeyUser, StopsRepresentation = journey.Stops }, serializeOptions),
            });
        }

        private async Task NotifyClientAsync(NotificationDto notification)
        {
            await notificationHub.Clients.All.SendAsync("sendToReact", notification);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await GetUnreadNotificationsNumberAsync());
        }

        private async Task NotifyDeleteClientAsync(int notificationId)
        {
            await notificationHub.Clients.All.SendAsync("deleteFromReact", notificationId);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await GetUnreadNotificationsNumberAsync());
        }
    }
}