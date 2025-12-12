using SaudiCitiesAI.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Interfaces
{
    public interface IAttractionService
    {
        Task<IEnumerable<AttractionDto>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default);
        Task<IEnumerable<AttractionDto>> SearchByNameAsync(string q, int limit = 50, CancellationToken ct = default);
    }
}