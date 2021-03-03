using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService modelService;

        public ModelController(IModelService modelService) => this.modelService = modelService;

        /// <summary>
        /// Gets all models.
        /// </summary>
        /// <param name="id">The brand identifier.</param>
        /// <returns>All the models</returns>
        [HttpGet("by-brand/{id}")]
        public async Task<IActionResult> GetAll(int id) =>
            Ok(await modelService.GetModelsByBrandIdAsync(id));
    }
}
