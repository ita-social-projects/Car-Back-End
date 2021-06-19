using System.Threading.Tasks;
using Car.Data.Entities;
using Car.Data.Infrastructure;
using FirebaseAdmin.Messaging;

namespace Car.Domain.Services.Interfaces
{
    public interface IFirebaseService
    {
        Task SendNotification(IRepository<User> userRepository, int id, string title, string body);

        Task UpdateUserToken(IRepository<User> userRepository, string token, int id);
    }
}
