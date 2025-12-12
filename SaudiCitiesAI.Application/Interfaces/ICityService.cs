using SaudiCitiesAI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllAsync(
            int page = 1,
            int pageSize = 50,
            CancellationToken ct = default);

        Task<CityDto?> GetByIdAsync(
            Guid id,
            CancellationToken ct = default);

        Task<IEnumerable<CityDto>> SearchByNameAsync(
            string name,
            int limit = 50,
            CancellationToken ct = default);
    }
}