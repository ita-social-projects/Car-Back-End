using Microsoft.AspNetCore.Mvc;
using Car.BLL.Services.Interfaces;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JourneyController : ControllerBase
    {
        private readonly IJourneyService journeyService;

        public JourneyController(IJourneyService journeyService)
        {
            this.journeyService = journeyService;
        }

        /// <summary>
        /// Gets journey by identifier.
        /// </summary>
        /// <param name="journeyId">The journey identifier.</param>
        /// <returns>The journey entity.</returns>
        [HttpGet("{journeyId}")]
        public IActionResult GetJourneyById(int journeyId)
        {
            return Ok(journeyService.GetJourneyById(journeyId));
        }
    }
}
