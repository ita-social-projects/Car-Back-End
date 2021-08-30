using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/journeyusers")]
    [ApiController]
    public class JourneyUserController : ControllerBase
    {
        private readonly IJourneyUserService journeyUserService;

        public JourneyUserController(IJourneyUserService journeyUserService)
        {
            this.journeyUserService = journeyUserService;
        }

        /// <summary>
        /// Get JourneyUser by identifiers.
        /// </summary>
        /// <param name="journeyId">Journey identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>JourneyUserDTO</returns>
        [HttpGet("{journeyId}/{userId}")]
        public async Task<IActionResult> GetJourneyUserById(int journeyId, int userId)
        {
            return Ok(await journeyUserService.GetJourneyUserByIdAsync(journeyId, userId));
        }

        /// <summary>
        /// Defines if JourneyUser has a baggage
        /// </summary>
        /// <param name="journeyId">Journey identifier</param>
        /// <param name="userId">User identifier</param>
        /// <returns>bool value that indicates whether JourneyUser has a baggage</returns>
        [HttpGet("has-baggage/{journeyId}/{userId}")]
        public async Task<IActionResult> HasBaggage(int journeyId, int userId)
        {
            return Ok(await journeyUserService.HasBaggage(journeyId, userId));
        }

        /// <summary>
        /// Updates the JourneyUser asynchronously
        /// </summary>
        /// <param name="journeyUser">JourneyUser DTO</param>
        /// <returns>OkResult with updated JourneyUser</returns>
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] JourneyUserDto journeyUser)
        {
            return Ok(await journeyUserService.UpdateJourneyUserAsync(journeyUser));
        }

        /// <summary>
        /// Set value of the withBaggage property
        /// </summary>
        /// <param name="journeyId">Journey identifier</param>
        /// <param name="userId">User identifier</param>
        /// <param name="withBaggage">New value of the withBaggage property</param>
        /// <returns>OkResult</returns>
        [HttpPut("{journeyId}/{userId}")]
        public async Task<IActionResult> UpdateWithBaggage(int journeyId, int userId, [FromBody]bool withBaggage)
        {
            return Ok(await journeyUserService.SetWithBaggageAsync(journeyId, userId, withBaggage));
        }
    }
}
