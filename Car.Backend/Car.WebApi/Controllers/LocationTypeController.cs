﻿using System.Threading.Tasks;
using Car.Domain.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace Car.WebApi.Controllers
{
    [Authorize]
    [RequiredScope("ApiAccess")]
    [Route("api/location-types")]
    [ApiController]
    public class LocationTypeController : ControllerBase
    {
        private readonly ILocationTypeService locationTypeService;

        public LocationTypeController(ILocationTypeService locationTypeService) =>
            this.locationTypeService = locationTypeService;

        /// <summary>
        /// Gets all location types.
        /// </summary>
        /// <returns>All the models</returns>
        [HttpGet("location-types/")]
        public async Task<IActionResult> GetAll() =>
            Ok(await locationTypeService.GetAllLocationTypesAsync());
    }
}
