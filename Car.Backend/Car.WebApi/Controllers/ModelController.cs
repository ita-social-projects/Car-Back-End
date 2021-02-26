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
        public IActionResult GetAll(int id) => Ok(modelService.GetModelsByBrandId(id));
    }
}
