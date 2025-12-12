using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;

namespace SaudiCitiesAI.Infrastructure.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(SaudiCitiesDbContext db) : base(db)
        {
        }

        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _set
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, ct);
        }

        public async Task<User?> GetByApiKeyHashAsync(string apiKeyHash, CancellationToken ct = default)
        {
            return await _set
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.ApiKeyHash == apiKeyHash, ct);
        }
    }
}
