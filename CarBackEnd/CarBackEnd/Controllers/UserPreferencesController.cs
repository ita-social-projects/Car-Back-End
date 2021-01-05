using Microsoft.AspNetCore.Mvc;
using Car.BLL.Services.Interfaces;
using Car.DAL.Entities;
using Microsoft.AspNetCore.Authorization;

namespace CarBackEnd.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IPreferencesService _preferencesService;

        public UserPreferencesController(IPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        /// <summary>
        /// returns the preferences for user
        /// </summary>
        /// <param name ="userId"> userId</param>
        /// <returns>user preferences</returns>
        [HttpGet("{userId}")]
        public IActionResult GetPreferences(int userId)
        {
            return Ok(_preferencesService.GetPreferences(userId));
        }

        /// <summary>
        /// updates preferences
        /// </summary>
        /// <param name="preferences">preferences to be updated</param>
        /// <returns>updated preference</returns>
        [HttpPut]
        public IActionResult UpdatePreferences([FromBody] UserPreferences preferences)
        {
            return Ok(_preferencesService.UpdatePreferences(preferences));
        }
    }
}
