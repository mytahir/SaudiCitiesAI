using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Api.DTOs.Requests;
using SaudiCitiesAI.Api.DTOs.Responses;

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
            var result = await _aiService.GenerateCityInsightByNameAsync(
                request.CityName,
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