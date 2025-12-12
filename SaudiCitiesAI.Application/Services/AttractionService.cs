using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Application.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository _attractionRepo;

        public AttractionService(IAttractionRepository attractionRepo)
        {
            _attractionRepo = attractionRepo;
        }

        public async Task<IEnumerable<AttractionDto>> GetByCityIdAsync(Guid cityId, CancellationToken ct = default)
        {
            var list = await _attractionRepo.GetByCityIdAsync(cityId, ct);
            return list.Select(Map);
        }

        public async Task<IEnumerable<AttractionDto>> SearchByNameAsync(string q, int limit = 50, CancellationToken ct = default)
        {
            var list = await _attractionRepo.SearchByNameAsync(q, ct);
            return list.Take(limit).Select(Map);
        }

        private AttractionDto Map(Domain.Entities.Attraction a)
        {
            return new AttractionDto
            {
                Id = a.Id,
                CityId = a.CityId,
                Name = a.Name,
                Category = a.Category.ToString(),
                Latitude = Convert.ToDecimal(a.Coordinates.Latitude),
                Longitude = Convert.ToDecimal(a.Coordinates.Longitude),
                Description = a.Description
            };
        }
    }
}
