using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;
using System.Linq.Expressions;

namespace SaudiCitiesAI.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : class
    {
        protected readonly SaudiCitiesDbContext _db;
        protected readonly DbSet<TEntity> _set;

        protected RepositoryBase(SaudiCitiesDbContext db)
        {
            _db = db;
            _set = _db.Set<TEntity>();
        }

        public virtual async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _set.FindAsync(new object[] { id }, ct);
        }

        public virtual async Task<List<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken ct = default)
        {
            return await _set.AsNoTracking().ToListAsync(ct);
        }

        public virtual async Task<List<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>> predicate,
            CancellationToken ct = default)
        {
            return await _set.AsNoTracking().Where(predicate).ToListAsync(ct);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
        {
            await _set.AddAsync(entity, ct);
        }

        public virtual void Update(TEntity entity)
        {
            _set.Update(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            _set.Remove(entity);
        }

        public virtual async Task SaveChangesAsync(CancellationToken ct = default)
        {
            await _db.SaveChangesAsync(ct);
        }
    }
}
