﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Models.Notification;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/notifications")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this.notificationService = notificationService;
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
        /// <returns>list of user notifications</returns>
        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotificationsAsync() =>
            Ok(await notificationService.GetNotificationsAsync());

        /// <summary>
        /// gets user unread notifications number
        /// </summary>
        /// <returns>int number</returns>
        [HttpGet("unreadNumber")]
        public async Task<IActionResult> GetUnreadNotificationsNumberAsync() =>
             Ok(await notificationService.GetUnreadNotificationsNumberAsync());

        /// <summary>
        /// adds new user notification Asynchronously
        /// </summary>
        /// <param name="createNotificationModel">notification itself to be updated</param>
        /// // Args:
        /// //    JSON String
        /// //
        /// // JSON STRUCTURE
        /// //      VARIABLE           TYPE            DESC
        /// // {
        /// //      "senderId":        //number        Specifies the sender of the notification
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
        /// //      , "jsonData":      //string        Specifies notification's specific JSON structure
        /// //                         //
        /// //                         //              BELOW LISTED STRUCTURE FOR NOTIFICATION TYPE 1 (PassengerApply)
        /// //      "{                 //
        /// //        \"title\":       //string        Specifies the title of the notification at the notification tab
        /// //        \"comments\":    //string?       Specifies the participant's comment (if any) at the modal page
        /// //        \"hasLuggage\":  //boolean?      Specifies if the participant has any luggage
        /// //      }"
        /// // }
        /// <returns>added notification</returns>
        [HttpPost]
        public async Task<IActionResult> AddNotificationAsync([FromBody] CreateNotificationDto createNotificationModel)
        {
            var notification = await notificationService.CreateNewNotificationAsync(createNotificationModel);
            await notificationService.AddNotificationAsync(notification);
            return Ok(notification);
        }

        /// <summary>
        /// deletes notification by identifier
        /// </summary>
        /// <param name="id">notification Id</param>
        /// <returns>ok</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            bool isNofiticationDeleted = await notificationService.DeleteAsync(id);
            return isNofiticationDeleted ? Ok() : Forbid();
        }

        /// <summary>
        /// Marks notification as read Asynchronously
        /// </summary>
        /// <param name="notificationId">notification Id</param>
        /// <returns>amended notification</returns>
        [HttpPut("{notificationId}")]
        public async Task<IActionResult> MarkNotificationAsReadAsync(int notificationId)
        {
            var (isNotificationUpdated, updatedNotification) = await notificationService.MarkNotificationAsReadAsync(notificationId);
            return isNotificationUpdated ? Ok(updatedNotification) : Forbid();
        }
    }
}
