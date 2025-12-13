using AutoMapper;
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
        private readonly IMapper _mapper;

        public AttractionService(
            IAttractionRepository attractionRepository,
            IMapper mapper)
        {
            _attractionRepository = attractionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AttractionDto>> GetByCityIdAsync(
            Guid cityId,
            CancellationToken ct = default)
        {
            var results = await _attractionRepository.GetByCityIdAsync(cityId);

            return _mapper.Map<IEnumerable<AttractionDto>>(results);
        }

        public async Task<IEnumerable<AttractionDto>> SearchByNameAsync(
            string q,
            int limit = 50,
            CancellationToken ct = default)
        {
            var results = await _attractionRepository.SearchByNameAsync(q);

            return _mapper.Map<IEnumerable<AttractionDto>>(results.Take(limit));
        }
    }
}
