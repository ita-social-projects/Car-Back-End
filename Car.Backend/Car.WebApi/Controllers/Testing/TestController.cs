using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/testing")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("{text}")]
        public string Do(string text)
        {
            return text + " it just works";
        }
    }
}