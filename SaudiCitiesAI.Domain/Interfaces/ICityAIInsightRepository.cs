using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Domain.Interfaces
{
    public interface ICityAIInsightRepository
    {
        Task AddAsync(CityAIInsight insight, CancellationToken ct);
        Task<CityAIInsight?> GetLatestAsync(
            string cityName,
            string mode,
            CancellationToken ct);
    }
}