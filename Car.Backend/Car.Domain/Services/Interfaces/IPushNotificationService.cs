using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using Car.Domain.Dto;

namespace Car.Domain.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task SendNotificationAsync(NotificationDto notification);

        Task SendNotificationAsync(Message message);
    }
}