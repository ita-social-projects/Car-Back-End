using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/user-preferences")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferencesService preferencesService;

        public UserPreferencesController(IUserPreferencesService preferencesService) =>
            this.preferencesService = preferencesService;

        /// <summary>
        /// returns the preferences for user
        /// </summary>
        /// <param name ="id">User identifier</param>
        /// <returns>user preferences</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPreferences(int id) =>
            Ok(await preferencesService.GetPreferencesAsync(id));

        /// <summary>
        /// updates preferences
        /// </summary>
        /// <param name="preferences">preferences to be updated</param>
        /// <returns>updated preference</returns>
        [HttpPut]
        public async Task<IActionResult> UpdatePreferences([FromBody] UserPreferencesDto preferences)
        {
            var (isPreferencesUpdated, updatedPreferences) = await preferencesService.UpdatePreferencesAsync(preferences);
            return isPreferencesUpdated ? Ok(updatedPreferences) : Forbid();
        }
    }
}
