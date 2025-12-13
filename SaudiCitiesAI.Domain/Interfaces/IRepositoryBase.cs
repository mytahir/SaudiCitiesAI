using System.Linq.Expressions;

namespace SaudiCitiesAI.Domain.Interfaces;

public interface IRepositoryBase<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<List<TEntity>> GetAllAsync(int page, int pageSize, CancellationToken ct = default);

    Task<List<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken ct = default);

    Task AddAsync(TEntity entity, CancellationToken ct = default);

    void Update(TEntity entity);

    void Remove(TEntity entity);

    Task SaveChangesAsync(CancellationToken ct = default);
}