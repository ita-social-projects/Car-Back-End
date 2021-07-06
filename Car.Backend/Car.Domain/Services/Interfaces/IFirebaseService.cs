using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;

namespace Car.Domain.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task SendNotification(Notification notification);

        Task SendNotification(Message message);

        Task UpdateUserToken(string token, int id);
    }
}
