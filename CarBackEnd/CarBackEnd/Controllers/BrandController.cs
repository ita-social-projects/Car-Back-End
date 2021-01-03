using Car.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        /// <summary>
        /// Gets the brands.
        /// </summary>
        /// <returns>The brands</returns>
        [HttpGet]
        public IActionResult GetBrands()
        {
            return Ok(brandService.GetAllBrands());
        }
    }
}
