using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [Route("api/user-preferences")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IPreferencesService preferencesService;

        public UserPreferencesController(IPreferencesService preferencesService) =>
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
        public async Task<IActionResult> UpdatePreferences([FromBody] UserPreferencesDto preferences) =>
            Ok(await preferencesService.UpdatePreferencesAsync(preferences));
    }
}
