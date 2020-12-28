using System;
using System.Text;
using System.Threading.Tasks;
using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using MimeKit;
using Car.BLL.Dto;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTestController : ControllerBase
    {
        private readonly IEmailSenderService _emailSender;

        public EmailTestController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        // POST api/<EmailTestController>
        [HttpPost]
        public async Task Post([FromForm] FormImage userFile)
        {
            // put email you want to receive message
            var passanger1 = new Car.DAL.Entities.User
            {
                Email = "gavrylyakbogdan@gmail.com",
                Name = "Name1",
                Surname = "Surname1",
            };
            var passanger2 = new Car.DAL.Entities.User
            {
                Email = "bogdangavrylyak@ukr.net",
                Name = "Name2",
                Surname = "Surname2",
            };
            var driver1 = new Car.DAL.Entities.User
            {
                Email = "driver1@gmail.com",
                Name = "Name3",
                Surname = "Surname3",
            };
            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress>
            {
                new MailboxAddress(Encoding.UTF8, passanger1.Name, passanger1.Email),
                new MailboxAddress(Encoding.UTF8, passanger2.Name, passanger2.Email),
            };
            Message mailingMessage = new Message
            {
                Recipients = mailboxAddresses,
                DriverAddress = new MailboxAddress(Encoding.UTF8, driver1.Name, driver1.Email),
                CancelDate = DateTime.Now,
                Attachments = userFile.image,
            };

            await _emailSender.CancelJourneyAsync(mailingMessage);
        }
    }
}