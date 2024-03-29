﻿using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService) =>
            this.brandService = brandService;

        /// <summary>
        /// Gets the brands.
        /// </summary>
        /// <returns>The brands</returns>
        [HttpGet]
        public async Task<IActionResult> GetBrands() =>
             Ok(await brandService.GetAllAsync());
    }
}
