using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Car.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly IModelService modelService;

        public ModelController(IModelService modelService)
        {
            this.modelService = modelService;
        }

        /// <summary>
        /// Gets all models.
        /// </summary>
        /// <param name="brandId">The brand identifier.</param>
        /// <returns>All the models</returns>
        [HttpGet("brand/{brandId}")]
        public IActionResult GetAll(int brandId)
        {
            return Ok(modelService.GetModelsByBrandId(brandId));
        }
    }
}
