using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Car.WebApi.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Car.WebApi.Controllers
{
    // [Authorize]
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
        public IActionResult GetNotification(int id)
        {
            Notification notification = notificationService.GetNotification(id);
            if (notification == null)
            {
                return BadRequest();
            }

            NotificationDto notificationDto = new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                UserName = notification.User.Name + " " + notification.User.Surname,
                Position = notification.User.Position,
                Description = notification.Description,
                IsRead = notification.IsRead,
                CreateAt = GetTimeDifference(notification.CreateAt),
                JourneyId = notification.JourneyId,
                ReceiverId = notification.ReceiverId,
                NotificationType = (NotificationType)notification.NotificationTypeId,
            };
            return Ok(notificationDto);
        }

        /// <summary>
        /// gets all user notifications
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <returns>list of user notifications</returns>
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
                    UserId = item.UserId,
                    UserName = item.User.Name + " " + item.User.Surname,
                    Position = item.User.Position,
                    Description = item.Description,
                    IsRead = item.IsRead,
                    CreateAt = GetTimeDifference(item.CreateAt),
                    JourneyId = item.JourneyId,
                    ReceiverId = item.ReceiverId,
                    UserColor = GetUserColor(item.User.Name + " " + item.User.Surname),
                    NotificationType = (NotificationType)item.NotificationTypeId,
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
                UserId = notificationDto.UserId,
                Description = notificationDto.Description,
                CreateAt = DateTime.Now,
                IsRead = notificationDto.IsRead,
                JourneyId = notificationDto.JourneyId,
                ReceiverId = notificationDto.ReceiverId,
                NotificationTypeId = (int)notificationDto.NotificationType,
            };

            Notification notificationSaved = notificationService.AddNotification(notification);
            NotificationDto notification_Dto = new NotificationDto
            {
                Id = notificationSaved.Id,
                UserId = notificationSaved.UserId,
                CreateAt = GetTimeDifference(notificationSaved.CreateAt),
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

        private static string GetUserColor(string name)
        {
            var names = name.ToLower().Split(' ');
            return "rgb(" + Math.Floor(255 - (((int)names[0][0] - 97) * 10.2)) + "," +
                Math.Floor(((int)names[0][0] - 97) * 10.2) + "," +
                Math.Floor(255 - (((int)names[names.Length - 1][0] - 97) * 10.2)) + ")";
        }

        private static string GetTimeDifference(DateTime creationTime)
        {
            TimeSpan differ = DateTime.Now - creationTime;
            if (differ.TotalMinutes < 60)
            {
                if (differ.Minutes == 0)
                {
                    return "now";
                }

                return differ.Minutes + " m";
            }
            else if (differ.TotalHours < 24)
            {
                return differ.Hours + " h";
            }
            else if (differ.TotalDays < 30)
            {
                return differ.Days + " d";
            }
            else if (differ.TotalDays < 365)
            {
                return (int)(differ.TotalDays / 30) + " m";
            }
            else
            {
                return creationTime.ToShortDateString();
            }
        }
    }
}
