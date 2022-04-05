using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/userstatistic")]
    [ApiController]
    public class UserStatisticController : ControllerBase
    {
        private readonly IBadgeService badgeService;

        public UserStatisticController(IBadgeService badgeService)
        {
            this.badgeService = badgeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserStatisticById(int id)
        {
            return Ok(await badgeService.GetUserStatisticByUserIdAsync(id));
        }
    }
}
