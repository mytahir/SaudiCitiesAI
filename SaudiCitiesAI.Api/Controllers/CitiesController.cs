using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Api.DTOs.Responses;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Utils;

namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Get all Saudi cities (paginated)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 50,
            CancellationToken ct = default)
        {
            var cities = await _cityService.GetAllAsync(page, pageSize, ct);

            var response = cities.Select(c => new CityResponse
            {
                Id = c.Id,
                Name = c.Name,
                Region = c.Region,
                Latitude = c.Latitude,
                Longitude = c.Longitude
            });

            return Ok(response);
        }

        /// <summary>
        /// Get city by ID
        /// </summary>
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        {
            var city = await _cityService.GetByIdAsync(id, ct);

            if (city == null)
                return NotFound();

            return Ok(new CityResponse
            {
                Id = city.Id,
                Name = city.Name,
                Region = city.Region,
                Latitude = city.Latitude,
                Longitude = city.Longitude
            });
        }

        /// <summary>
        /// Search cities by name
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string q,
            [FromQuery] int limit = 50,
            CancellationToken ct = default)
        {
            // Normalize input: convert English variant to Arabic
            var arabicCityName = SaudiCityNameMapper.GetArabicName(q);

            var cities = await _cityService.SearchByNameAsync(arabicCityName, limit, ct);

            var response = cities.Select(c => new CityResponse
            {
                Id = c.Id,
                Name = c.Name,
                Region = c.Region,
                Latitude = c.Latitude,
                Longitude = c.Longitude
            });

            return Ok(response);
        }
    }
}