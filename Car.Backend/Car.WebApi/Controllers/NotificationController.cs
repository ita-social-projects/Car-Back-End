﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Car.Domain.Models.Notification;
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
        private readonly IHubContext<SignalRHub> notificationHub;
        private readonly INotificationService notificationService;

        public NotificationController(
            INotificationService notificationService,
            [NotNull] IHubContext<SignalRHub> notificationHub)
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
        /// <param name="createNotificationModel">notification itself to be updated</param>
        /// <returns>updated notification</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateNotificationAsync([FromBody] CreateNotificationModel createNotificationModel)
        {
            var notification = await notificationService.CreateNewNotificationAsync(createNotificationModel);
            await notificationService.UpdateNotificationAsync(notification);
            await notificationHub.Clients.All.SendAsync("sendToReact", notification);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await notificationService.GetUnreadNotificationsNumberAsync(notification.ReceiverId));
            return Ok(notification);
        }

        /// <summary>
        /// adds new user notification Asynchronously
        /// </summary>
        /// // Args:
        /// //    JSON String
        /// //
        /// // JSON STRUCTURE
        /// //      VARIABLE           TYPE            DESC
        /// // {
        /// //      "senderId"         //number        Specifies the sender of the notification
        /// //      , "receiverId":    //number        Specifies the receiver of the notification
        /// //      , "type":          //number        Specifies the type of the notification
        /// //                         //              based on the following enum:
        /// //                         //
        /// //                         //              1 PassengerApply
        /// //                         //              2 ApplicationApproval
        /// //                         //              3 JourneyCancellation
        /// //                         //              4 JourneyDetailsUpdate
        /// //                         //              5 JourneyInvitation
        /// //                         //              6 AcceptedInvitation
        /// //                         //              7 RejectedInvitation
        /// //                         //              8 PassengerWithdrawal
        /// //                         //              9 HRMarketingMessage
        /// //                         //              0 HRMarketingSurvey
        /// //                         //
        /// //     , "jsonData":       //string        Specifies notification's specific JSON structure
        /// //                         //
        /// //                         //              BELOW LISTED STRUCTURE FOR NOTIFICATION TYPE 1 (PassengerApply)
        /// //     "{                  //
        /// //     \"title\":          //string        Specifies the title of the notification at the notification tab
        /// //     \"comments\":       //string?       Specifies the participant's comment (if any) at the modal page
        /// //     \"hasLuggage\":     //boolean?      Specifies if the participant has any luggage
        /// //     }"
        /// // }
        /// <returns>added notification</returns>
        [HttpPost]
        public async Task<IActionResult> AddNotificationAsync([FromBody] CreateNotificationModel createNotificationModel)
        {
            var notification = await notificationService.CreateNewNotificationAsync(createNotificationModel);
            await notificationService.AddNotificationAsync(notification);
            await notificationHub.Clients.All.SendAsync("sendToReact", notification);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await notificationService.GetUnreadNotificationsNumberAsync(notification.ReceiverId));
            return Ok(notification);
        }

        /// <summary>
        /// deletes notification Asynchronously
        /// </summary>
        /// <param name="notificationId">notification Id</param>
        /// <returns>deleted notification</returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteNotificationAsync([FromBody] int notificationId) =>
            Ok(await notificationService.DeleteNotificationAsync(notificationId));

        /// <summary>
        /// Marks notification as read Asynchronously
        /// </summary>
        /// <param name="notificationId">notification Id</param>
        /// <returns>amended notification</returns>
        [HttpPut("{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await notificationService.MarkNotificationAsReadAsync(notificationId);
            await notificationHub.Clients.All.SendAsync("sendToReact", notification);
            await notificationHub.Clients.All.SendAsync(
                "updateUnreadNotificationsNumber",
                await notificationService.GetUnreadNotificationsNumberAsync(notification.ReceiverId));
            return Ok(notification);
        }
    }
}
