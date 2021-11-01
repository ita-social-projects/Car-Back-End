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
    [Route("api/requests")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestService requestService;

        public RequestController(IRequestService requestService)
        {
            this.requestService = requestService;
        }

        /// <summary>
        /// Gets the request by identifier.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>Request.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Ok(await requestService.GetRequestByIdAsync(id));

        /// <summary>
        /// Adds new request.
        /// </summary>
        /// <param name="request">Request that will be added.</param>
        /// <returns>Added request.</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RequestDto request) =>
            Ok(await requestService.AddRequestAsync(request));

        /// <summary>
        /// Removes request by its identifier.
        /// </summary>
        /// <param name="id">Unique identifier.</param>
        /// <returns>Ok result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool isRequestDeleted = await requestService.DeleteAsync(id);
            return isRequestDeleted ? Ok() : Forbid();
        }

        /// <summary>
        /// Gets requests of current user.
        /// </summary>
        /// <returns>Collection of requests</returns>
        [HttpGet("by-user")]
        public async Task<IActionResult> GetByUserId() =>
            Ok(await requestService.GetRequestsByUserIdAsync());
    }
}
