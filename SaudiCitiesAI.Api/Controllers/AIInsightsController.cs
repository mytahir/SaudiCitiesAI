using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Api.DTOs.Requests;
using SaudiCitiesAI.Api.DTOs.Responses;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Utils;

namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/ai/insights")]
    public class AIInsightsController : ControllerBase
    {
        private readonly IAIInsightService _aiService;

        public AIInsightsController(IAIInsightService aiService)
        {
            _aiService = aiService;
        }

        /// <summary>
        /// Generate AI insight for a city
        /// </summary>
        [HttpPost("city/{cityId:guid}")]
        public async Task<IActionResult> GenerateCityInsight(
            Guid cityId,
            [FromBody] AIPromptRequest request,
            CancellationToken ct)
        {
            var result = await _aiService.GenerateCityInsightAsync(
                cityId,
                request.Mode,
                request.UserId,
                ct);

            return Ok(new AISummaryResponse
            {
                Content = result.Content
            });
        }

        [HttpPost("city/search")]
        public async Task<IActionResult> GenerateCityInsightByName(
            [FromBody] AIPromptRequest request,
            CancellationToken ct)
        {
            // Normalize input: convert English variant to Arabic
            var arabicCityName = SaudiCityNameMapper.GetArabicName(request.CityName);

            var result = await _aiService.GenerateCityInsightByNameAsync(
                arabicCityName,
                request.Mode,
                request.UserId,
                ct);

            return Ok(new AISummaryResponse
            {
                Content = result.Content
            });
        }


    }
}