using Car.BLL.Dto.Email;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task CancelJourneyAsync(Message mailingMessage);
    }
}
