using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cities/{cityId:guid}/attractions")]
    public class AttractionsController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        public AttractionsController(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetByCity(Guid cityId)
        {
            var list = await _attractionService.GetByCityIdAsync(cityId);
            return Ok(list);
        }

        [HttpGet("search")]
        [Route("/api/v1/attractions/search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return BadRequest("q is required");
            var list = await _attractionService.SearchByNameAsync(q);
            return Ok(list);
        }
    }
}
