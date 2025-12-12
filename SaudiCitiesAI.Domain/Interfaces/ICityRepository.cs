using SaudiCitiesAI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Interfaces
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        Task<List<City>> ListAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<City?> GetCityWithDetailsAsync(
            Guid id,
            CancellationToken ct = default);

        Task<List<City>> SearchByNameAsync(
            string name,
            CancellationToken ct = default);
    }
}