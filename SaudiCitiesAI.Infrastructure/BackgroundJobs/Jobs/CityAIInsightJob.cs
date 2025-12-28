using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;

namespace SaudiCitiesAI.Infrastructure.BackgroundJobs.Jobs
{
    public class CityAIInsightJob : ICityAIInsightJob
    {
        private const int PageSize = 100;

        private readonly ICityRepository _cityRepository;
        private readonly ICityAIInsightRepository _insightRepository;
        private readonly IAIInsightService _aiInsightService;

        public CityAIInsightJob(
            ICityRepository cityRepository,
            ICityAIInsightRepository insightRepository,
            IAIInsightService aiInsightService)
        {
            _cityRepository = cityRepository;
            _insightRepository = insightRepository;
            _aiInsightService = aiInsightService;
        }

        public async Task RefreshDailyInsightsAsync(CancellationToken ct)
        {
            var page = 1;

            while (true)
            {
                var cities = await _cityRepository
                    .GetAllAsync(page, PageSize, ct);

                if (cities.Count == 0)
                    break;

                foreach (var city in cities)
                {
                    var latest = await _insightRepository.GetLatestAsync(
                        city.Name,
                        "daily",
                        ct);

                    if (latest != null &&
                        latest.GeneratedAt >= DateTime.UtcNow.AddDays(-1))
                        continue;

                    var aiResult =
                        await _aiInsightService.GenerateCityInsightAsync(
                            city.Id,
                            mode: "daily",
                            ct: ct);

                    var insight = new CityAIInsight(
                        city.Id,
                        city.Name,
                        "daily",
                        aiResult.Content);

                    await _insightRepository.AddAsync(insight, ct);
                }

                page++;
            }
        }
    }
}