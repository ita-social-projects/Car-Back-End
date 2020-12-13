using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.BLL.Dto.Email;
using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /*private readonly IEmailSenderService _emailSender;

        public ValuesController(IEmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpGet("{id}")]
        public async Task Get(int id)
        {
            //Car.BLL.Services.Implementation.EmailService emailService = new Car.BLL.Services.Implementation.EmailService();
            //await emailService.SendEmailAsync("gavrylyakbogdan@gmail.com", "subjectHTML",
            //    "<div align='center'>" +
            //        "<h1 style='color: red'>Message HTML</h1>" +
            //    "</div>");

            var message = new Message(new string[] { "gavrylyakbogdan@gmail.com" },
                "Test mail async", "<div align = 'center' > " +
                    "<h1 style='color:DodgerBlue'>Message HTML</h1>" +
                "</div>");

            await _emailSender.SendEmailAsync(message);
        }*/

        // GET: api/<ValuesController>
        /*[HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
