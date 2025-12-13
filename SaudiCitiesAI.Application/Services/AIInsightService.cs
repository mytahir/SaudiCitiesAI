using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Domain.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Services
{
    public class AIInsightService : IAIInsightService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILongCatAIService _aiService;

        public AIInsightService(
            ICityRepository cityRepository,
            ILongCatAIService aiService)
        {
            _cityRepository = cityRepository;
            _aiService = aiService;
        }

        public async Task<AIGeneratedContentDto> GenerateCityInsightAsync(
            Guid cityId,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default)
        {
            var city = await _cityRepository.GetByIdAsync(cityId, ct);

            if (city == null)
            {
                throw new NotFoundException($"City with id '{cityId}' was not found.");
            }

            var prompt = BuildPrompt(city.Name, city.Region.Name, mode);

            var aiContent = await _aiService.GenerateAsync(prompt, ct);

            return new AIGeneratedContentDto
            {
                Content = aiContent
            };
        }

        private static string BuildPrompt(
            string cityName,
            string regionName,
            string mode)
        {
            var sb = new StringBuilder();

            sb.AppendLine($"You are an expert assistant on Saudi Arabia.");
            sb.AppendLine($"Provide a {mode} insight for the city of {cityName}.");
            sb.AppendLine($"Region: {regionName}.");
            sb.AppendLine($"Align the response with Saudi Vision 203_toggle.");
            sb.AppendLine($"Keep the answer concise, informative, and factual.");

            return sb.ToString();
        }
    }
}