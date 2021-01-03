using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarBackEnd.Controllers
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
        /// Get the models
        /// </summary>
        /// <returns>The models</returns>
        [HttpGet]
        public IActionResult GetModels()
        {
            return Ok(modelService.GetModels());
        }
    }
}
