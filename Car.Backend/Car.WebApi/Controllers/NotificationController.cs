using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Car.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> notificationHub;

        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService, [NotNull] IHubContext<NotificationHub> notificationHub)
        {
            this.notificationService = notificationService;
            this.notificationHub = notificationHub;
        }

        /// <summary>
        /// gets a notification by given id
        /// </summary>
        /// <param name="id">notification id</param>
        /// <returns>notification</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotification(int id) => Ok(await notificationService.GetNotificationAsync(id));

        /// <summary>
        /// gets all user notifications
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <returns>list of user notifications</returns>
        [HttpGet("notifications/s{userId}")]

        public async Task<IActionResult> GetNotificationsAsync(int userId) =>
            Ok(await notificationService.GetNotificationsAsync(userId));

        [HttpGet("notifications/{userId}")]
        public IActionResult GetNotifications(int userId)
        {
            List<Notification> notifications = notificationService.GetNotifications(userId);
            if (notifications == null)
            {
                return Ok(new List<NotificationDto>());
            }

            List<NotificationDto> notificationDtos = new List<NotificationDto>();
            foreach (var item in notifications)
            {
                NotificationDto notificationDto = new NotificationDto
                {
                    Id = item.Id,
                    UserId = item.SenderId,
                    UserName = item.Sender.Name + " " + item.Sender.Surname,
                    Position = item.Sender.Position,
                    Description = item.Description,
                    IsRead = item.IsRead,
                    CreateAt = item.CreatedAt,
                    JourneyId = item.JourneyId,
                    ReceiverId = item.ReceiverId,
                    NotificationType = (NotificationType)item.Type,
                };
                notificationDtos.Add(notificationDto);
            }

            return Ok(notificationDtos);
        }

        /// <summary>
        /// gets user unread notifications number
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>int number</returns>
        [HttpGet("unreadNumber/{userId}")]
        public IActionResult GetUnreadNumber(int userId)
        {
            return Ok(notificationService.GetUnreadNotificationsNumber(userId));
        }

        /// <summary>
        /// Marks notification as read
        /// </summary>
        /// <param name="notificationId">notificationId Id</param>
        /// <returns>Notification</returns>
        [HttpPost("{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId) =>
            Ok(await notificationService.MarkAsReadAsync(notificationId));

        /// <summary>
        /// updates user notification
        /// </summary>
        /// <param name="notificationDto">notification itself to be updated</param>
        /// <returns>updated notification</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNotification([FromBody] NotificationDto notificationDto)
        {
            var notification = notificationService.GetNotification(notificationDto.Id);
            notification.IsRead = notificationDto.IsRead;
            var notificationUpdated = notificationService.UpdateNotification(notification);
            await this.notificationHub.Clients.All.SendAsync("sendToReact", "The message");
            return Ok(notificationUpdated);
        }

        /// <summary>
        /// adds new user notification
        /// </summary>
        /// <param name="notificationDto">notification to be added</param>
        /// <returns>added notification</returns>
        [HttpPost]
        public async Task<IActionResult> AddNotification([FromBody] NotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest();
            }

            Notification notification = new Notification
            {
                SenderId = notificationDto.UserId,
                Description = notificationDto.Description,
                CreatedAt = DateTime.Now,
                IsRead = notificationDto.IsRead,
                JourneyId = notificationDto.JourneyId,
                ReceiverId = notificationDto.ReceiverId,
                Type = notificationDto.NotificationType,
            };

            Notification notificationSaved = notificationService.AddNotification(notification);
            NotificationDto notification_Dto = new NotificationDto
            {
                Id = notificationSaved.Id,
                UserId = notificationSaved.SenderId,
                CreateAt = notificationSaved.CreatedAt,
                Description = notificationSaved.Description,
                IsRead = notificationSaved.IsRead,
                JourneyId = notificationDto.JourneyId,
                ReceiverId = notificationDto.ReceiverId,
                NotificationType = notificationDto.NotificationType,
            };

            await this.notificationHub.Clients.All.SendAsync("sendToReact", "The message");
            return Ok(notification_Dto);
        }

        /// <summary>
        /// deletes notification
        /// </summary>
        /// <param name="notificationId">notification Id</param>
        /// <returns>deleted notification</returns>
        [HttpDelete]
        public IActionResult DeleteNotification([FromBody] int notificationId)
        {
            return Ok(notificationService.DeleteNotification(notificationId));
        }
    }
}
