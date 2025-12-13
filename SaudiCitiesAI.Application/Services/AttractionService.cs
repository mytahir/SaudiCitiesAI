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
        private readonly IAttractionRepository _attractionRepository;

        public AttractionService(IAttractionRepository attractionRepository)
        {
            _attractionRepository = attractionRepository;
        }

        public async Task<IEnumerable<AttractionDto>> GetByCityIdAsync(
            Guid cityId,
            CancellationToken ct = default)
        {
            var attractions = await _attractionRepository
                .GetByCityIdAsync(cityId, ct);

            return attractions.Select(MapToDto);
        }

        public async Task<IEnumerable<AttractionDto>> SearchByNameAsync(
            string q,
            int limit = 50,
            CancellationToken ct = default)
        {
            var attractions = await _attractionRepository
                .SearchByNameAsync(q, limit, ct);

            return attractions.Select(MapToDto);
        }

        private static AttractionDto MapToDto(Domain.Entities.Attraction attraction)
        {
            return new AttractionDto
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Category = attraction.Category.ToString(),
                Description = attraction.Description,
                CityId = attraction.CityId
            };
        }
    }
}