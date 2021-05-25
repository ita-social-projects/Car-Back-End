using System.Threading.Tasks;
using Car.Domain.Dto;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Authorize]
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
            await requestService.DeleteAsync(id);
            return Ok();
        }

        /// <summary>
        /// Gets requests by user identifier.
        /// </summary>
        /// <param name="id">User's unique identifier.</param>
        /// <returns>Collection of requests</returns>
        [HttpGet("by-user/{id}")]
        public async Task<IActionResult> GetByUserId(int id) =>
            Ok(await requestService.GetRequestsByUserIdAsync(id));
    }
}
