using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Api.DTOs.Responses;

namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/attractions")]
    public class AttractionsController : ControllerBase
    {
        private readonly IAttractionService _attractionService;

        public AttractionsController(IAttractionService attractionService)
        {
            _attractionService = attractionService;
        }

        /// <summary>
        /// Get attractions by city
        /// </summary>
        [HttpGet("city/{cityId:guid}")]
        public async Task<IActionResult> GetByCity(
            Guid cityId,
            CancellationToken ct)
        {
            var attractions = await _attractionService.GetByCityIdAsync(cityId, ct);

            var response = attractions.Select(a => new AttractionResponse
            {
                Id = a.Id,
                Name = a.Name,
                Category = a.Category,
                Description = a.Description
            });

            return Ok(response);
        }

        /// <summary>
        /// Search attractions by name
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string q,
            [FromQuery] int limit = 50,
            CancellationToken ct = default)
        {
            var attractions = await _attractionService.SearchByNameAsync(q, limit, ct);

            var response = attractions.Select(a => new AttractionResponse
            {
                Id = a.Id,
                Name = a.Name,
                Category = a.Category,
                Description = a.Description
            });

            return Ok(response);
        }
    }
}