using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using MimeKit;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using RazorClassLibraryForEmails.Views.Emails.CancelJourney;
using RazorClassLibraryForEmails.Services;

namespace Car.BLL.Services.Implementation
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly EmailConfiguration _emailConfig;
        private readonly ISmptClient _smtpClient;
        private readonly IRazorViewToStringRenderer _razorViewToStringRenderer;

        public EmailSenderService(
            EmailConfiguration emailConfig,
            ISmptClient smtpClient,
            IRazorViewToStringRenderer razorViewToStringRenderer)
        {
            _emailConfig = emailConfig;
            _smtpClient = smtpClient;
            _razorViewToStringRenderer = razorViewToStringRenderer;
        }

        [Obsolete]
        public async Task CancelJourneyAsync(Message message)
        {
            for (int i = 0; i < message.Recipients.Count; i++)
            {
                string body = await _razorViewToStringRenderer.RenderViewToStringAsync(
                "/Views/Emails/CancelJourney/CancelJourney.cshtml",
                new CancelJourneyViewModel
                {
                    PassangerName = message.Recipients[i].Name,
                    DriverName = message.DriverAddress.Name,
                    TimeOfStoppage = message.CancelDate,
                    Attachments = message.Attachments,
                });

                message.Content = body;
                message.Subject = $"Your journey was cancelled";

                await _smtpClient.SendAsync(CreateEmailMessage(message), _emailConfig);
            }
        }

        [Obsolete]
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