using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Domain.Interfaces
{
    public interface IAttractionRepository
    {
        Task<IEnumerable<Attraction>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default);
        Task<IEnumerable<Attraction>> SearchByNameAsync(string name, int limit, CancellationToken ct = default);
    }
}