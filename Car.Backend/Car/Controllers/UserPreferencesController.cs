using Car.Data.Entities;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.Controllers
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
        /// <param name ="id"> userId</param>
        /// <returns>user preferences</returns>
        [HttpGet("{id}")]
        public IActionResult GetPreferences(int id) => Ok(preferencesService.GetPreferences(id));

        /// <summary>
        /// updates preferences
        /// </summary>
        /// <param name="preferences">preferences to be updated</param>
        /// <returns>updated preference</returns>
        [HttpPut]
        public IActionResult UpdatePreferences([FromBody] UserPreferences preferences) =>
            Ok(preferencesService.UpdatePreferences(preferences));
    }
}
