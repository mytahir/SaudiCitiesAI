using Microsoft.AspNetCore.Mvc;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Api.DTOs.Requests;
using System;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cities/{cityId:guid}/ai")]
    public class AIInsightsController : ControllerBase
    {
        private readonly IAIInsightService _aiService;

        public AIInsightsController(IAIInsightService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("insights")]
        public async Task<IActionResult> GenerateInsight(Guid cityId, [FromBody] AIPromptRequest request)
        {
            if (request == null) return BadRequest();
            var dto = await _aiService.GenerateCityInsightAsync(cityId, request.Mode ?? "tourism");
            return Ok(dto);
        }
    }
}