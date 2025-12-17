using SaudiCitiesAI.Application.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface IAIInsightService
    {
        // DB-based insight
        Task<AIGeneratedContentDto> GenerateCityInsightAsync(
            Guid cityId,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default);

        // Search-based insight (OSM-powered)
        Task<AIGeneratedContentDto> GenerateCityInsightByNameAsync(
            string cityName,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default);
    }
}