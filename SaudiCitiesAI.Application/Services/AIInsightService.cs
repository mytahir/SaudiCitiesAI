using SaudiCitiesAI.AI.Clients;
using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;


namespace SaudiCitiesAI.Application.Services
{
    public class AIInsightService : IAIInsightService
    {
        private readonly ICityRepository _cityRepo;
        private readonly LongCatClient _longCatClient;

        public AIInsightService(ICityRepository cityRepo, LongCatClient longCatClient)
        {
            _cityRepo = cityRepo;
            _longCatClient = longCatClient;
        }

        public async Task<AIGeneratedContentDto> GenerateCityInsightAsync(Guid cityId, string mode = "tourism", Guid? userId = null, CancellationToken ct = default)
        {
            var city = await _cityRepo.GetCityWithDetailsAsync(cityId, ct);
            if (city == null) throw new KeyNotFoundException("City not found.");

            // Build a simple prompt from city facts (you can move to prompt builders)
            var prompt = $"Provide a {mode} insight for the city {city.Name} in Saudi Arabia.\n" +
                         $"Region: {city.Region?.Name}\n" +
                         $"Population: {city.Population ?? 0}\n" +
                         $"Top attractions: {string.Join(", ", city.Attractions?.Select(a => a.Name) ?? Array.Empty<string>())}\n" +
                         $"Vision2030 focuses: {string.Join(", ", city.VisionFocus?.Select(v => v.Category.ToString()) ?? Array.Empty<string>())}\n" +
                         "Be concise and include 3 actionable suggestions.";

            var raw = await _longCatClient.SendPromptAsync(prompt, ct);

            return new AIGeneratedContentDto
            {
                CityId = cityId,
                Mode = mode,
                Summary = TruncateSummary(raw, 2000),
                RawResponse = raw ?? string.Empty,
                GeneratedAt = DateTime.UtcNow
            };
        }

        private string TruncateSummary(string? text, int max)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return text.Length <= max ? text : text.Substring(0, max) + "...";
        }
    }
}