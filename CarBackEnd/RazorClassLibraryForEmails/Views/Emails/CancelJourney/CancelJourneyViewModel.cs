using Microsoft.AspNetCore.Http;
using System;

namespace RazorClassLibraryForEmails.Views.Emails.CancelJourney
{
    public class CancelJourneyViewModel
    {
        public string DriverName { get; set; }
        public DateTime TimeOfStoppage { get; set; }
        public IFormFile Attachments { get; set; }
    }
}
