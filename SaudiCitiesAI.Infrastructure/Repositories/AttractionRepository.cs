using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Enums;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;

namespace SaudiCitiesAI.Infrastructure.Repositories
{
    public class AttractionRepository : RepositoryBase<Attraction>, IAttractionRepository
    {
        public AttractionRepository(SaudiCitiesDbContext db) : base(db) { }

        public async Task<IEnumerable<Attraction>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default)
        {
            return await _set
                .Where(a => a.CityId == cityId)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Attraction>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            return await _set
                .Where(a => EF.Functions.Like(EF.Property<string>(a, "Name"), $"%{name}%"))
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IEnumerable<Attraction>> GetByCategoryAsync(AttractionCategory category, CancellationToken ct = default)
        {
            return await _set
                .Where(a => a.Category == category)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }

}
