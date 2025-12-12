using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;

namespace SaudiCitiesAI.Infrastructure.Repositories
{
    public class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(SaudiCitiesDbContext db) : base(db)
        {
        }

        public async Task<City?> GetCityWithDetailsAsync(Guid id, CancellationToken ct = default)
        {
            return await _set
                .Include("_attractions")
                .Include("_visionFocus")
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id, ct);
        }

        public async Task<List<City>> SearchByNameAsync(string name, CancellationToken ct = default)
        {
            return await _set
                .Where(c => EF.Functions.Like(EF.Property<string>(c, "Name"), $"%{name}%"))
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<List<City>> GetByRegionAsync(string regionName, CancellationToken ct = default)
        {
            return await _set
                .Where(c => EF.Property<string>(c, "RegionName") == regionName)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<List<City>> ListAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await _set
                .OrderBy(c => c.Name)                 
                .Skip((page - 1) * pageSize)          
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}
