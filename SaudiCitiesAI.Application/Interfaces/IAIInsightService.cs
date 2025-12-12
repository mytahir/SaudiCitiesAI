using SaudiCitiesAI.Application.DTOs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface IAIInsightService
    {
        Task<AIGeneratedContentDto> GenerateCityInsightAsync(Guid cityId, string mode = "tourism", Guid? userId = null, CancellationToken ct = default);
    }
}