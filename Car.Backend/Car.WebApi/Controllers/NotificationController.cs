using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        /// gets a notification by given id Asynchronously
        /// </summary>
        /// <param name="id">notification id</param>
        /// <returns>notification</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationAsync(int id) =>
            Ok(await notificationService.GetNotificationAsync(id));

        /// <summary>
        /// gets all user notifications
        /// </summary>
        /// <param name="userId"> user Id</param>
        /// <returns>list of user notifications</returns>
        [HttpGet("notifications/{userId}")]
        public async Task<IActionResult> GetNotificationsAsync(int userId) =>
            Ok(await notificationService.GetNotificationsAsync(userId));

        /// <summary>
        /// gets user unread notifications number
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>int number</returns>
        [HttpGet("unreadNumber/{userId}")]
        public async Task<IActionResult> GetUnreadNotificationsNumberAsync(int userId) =>
             Ok(await notificationService.GetUnreadNotificationsNumberAsync(userId));

        /// <summary>
        /// updates user notification Asynchronously
        /// </summary>
        /// <param name="notificationDto">notification itself to be updated</param>
        /// <returns>updated notification</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNotificationAsync([FromBody] NotificationDto notificationDto)
        {
            var notificationTask = notificationService.CreateNewNotificationFromDtoAsync(notificationDto);
            await this.notificationHub.Clients.All.SendAsync("sendToReact", await notificationTask);
            return Ok(await notificationTask);
        }

        /// <summary>
        /// adds new user notification Asynchronously
        /// </summary>
        /// <param name="notificationDto">notification to be added</param>
        /// <returns>added notification</returns>
        [HttpPost]
        public async Task<IActionResult> AddNotificationAsync([FromBody] NotificationDto notificationDto)
        {
            var notificationTask = notificationService.CreateNewNotificationFromDtoAsync(notificationDto);
            await this.notificationHub.Clients.All.SendAsync("sendToReact", await notificationTask);
            return Ok(await notificationTask);
        }

        /// <summary>
        /// deletes notification Asynchronously
        /// </summary>
        /// <param name="notificationId">notification Id</param>
        /// <returns>deleted notification</returns>
        [HttpDelete]
        public IActionResult DeleteNotificationAsync([FromBody] int notificationId)
        {
            return Ok(notificationService.DeleteNotificationAsync(notificationId));
        }
    }
}
