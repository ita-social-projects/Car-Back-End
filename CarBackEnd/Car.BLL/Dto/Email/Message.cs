using Microsoft.AspNetCore.Http;
using MimeKit;
using System;
using System.Collections.Generic;

namespace Car.BLL.Dto.Email
{
    public class Message
    {
        public List<MailboxAddress> Recipients { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public IFormFile Attachments { get; set; }

        public DateTime CancelDate { get; set; }

        public MailboxAddress DriverAddress { get; set; }
    }
}
