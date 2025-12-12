using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Application.Interfaces;


namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var list = await _cityService.GetAllAsync(page, pageSize);
            return Ok(list);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var city = await _cityService.GetByIdAsync(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q)) return BadRequest("q is required");
            var result = await _cityService.SearchByNameAsync(q);
            return Ok(result);
        }
    }
}
