using Car.BLL.Dto.Email;
using MimeKit;
using System.Threading.Tasks;

namespace Car.BLL.Services.Interfaces
{
    public interface ISmptClient
    {
        Task SendAsync(MimeMessage mailMessage, EmailConfiguration emailConfiguration);
    }
}
