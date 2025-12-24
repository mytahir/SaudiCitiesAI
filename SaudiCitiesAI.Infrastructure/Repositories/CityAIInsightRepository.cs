using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;

namespace SaudiCitiesAI.Infrastructure.Repositories
{
    public class CityAIInsightRepository
        : RepositoryBase<CityAIInsight>, ICityAIInsightRepository
    {
        public CityAIInsightRepository(SaudiCitiesDbContext db)
            : base(db)
        {
        }
        public async Task AddAsync(CityAIInsight insight, CancellationToken ct)
        {
            // Reuse base class AddAsync
            await base.AddAsync(insight, ct);
            await base.SaveChangesAsync(ct);
        }

        public async Task<CityAIInsight?> GetLatestAsync(string cityName, string mode, CancellationToken ct)
        {
            return await _set
                .AsNoTracking()
                .Where(c => c.CityName == cityName && c.Mode == mode)
                .OrderByDescending(c => c.GeneratedAt)
                .FirstOrDefaultAsync(ct);
        }
    }
}
