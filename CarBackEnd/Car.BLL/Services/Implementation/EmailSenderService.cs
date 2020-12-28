using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using MimeKit;
using System.Threading.Tasks;
using System.Net.Mail;
using RazorClassLibraryForEmails.Views.Emails.CancelJourney;
using RazorClassLibraryForEmails.Services;
using Microsoft.Extensions.Options;
using System.Linq;

namespace Car.BLL.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ISmptClient _smtpClient;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public EmailSenderService(
            IOptions<EmailConfiguration> emailConfig,
            ISmptClient smtpClient,
            IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailConfig = emailConfig.Value;
            _smtpClient = smtpClient;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        public async Task CancelJourneyAsync(Message message)
        {
            var body = await _razorViewToStringRenderer.RenderViewToStringAsync(
              "/Views/Emails/CancelJourney/CancelJourney.cshtml",
              new CancelJourneyViewModel
              {
                  DriverName = message.DriverAddress.Name,
                  TimeOfStoppage = message.CancelDate,
                  Attachments = message.Attachments,
              });

            message.Content = body;
            message.Subject = $"Your journey has been cancelled";

            await _smtpClient.SendAsync(CreateEmailMessage(message), _emailConfig);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailConfig.From),
                Subject = message.Subject,
            };

            message.Recipients.ForEach(recipient => mailMessage.To.Add(recipient.Address));

            AlternateView alternateView = AlternateView
                .CreateAlternateViewFromString(message.Content, null, "text/html");

            if (message.Attachments != null)
            {
                LinkedResource lr = new LinkedResource(message.Attachments.OpenReadStream());
                lr.ContentId = "imgpath";
                alternateView.LinkedResources.Add(lr);
            }

            mailMessage.AlternateViews.Add(alternateView);

            return (MimeMessage)mailMessage;
        }
    }
}