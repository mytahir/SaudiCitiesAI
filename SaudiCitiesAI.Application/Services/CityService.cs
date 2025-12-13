using AutoMapper;
using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;

namespace SaudiCitiesAI.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly IMapper _mapper;

        public CityService(
            ICityRepository cityRepository,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync(
            int page = 1,
            int pageSize = 50,
            CancellationToken ct = default)
        {
            var all = await _cityRepository.GetAllAsync();

            var paginated = all
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            return _mapper.Map<IEnumerable<CityDto>>(paginated);
        }

        public async Task<CityDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _cityRepository.GetByIdAsync(id);

            return entity == null
                ? null
                : _mapper.Map<CityDto>(entity);
        }

        public async Task<IEnumerable<CityDto>> SearchByNameAsync(
            string name,
            int limit = 50,
            CancellationToken ct = default)
        {
            var results = await _cityRepository.SearchByNameAsync(name);

            return _mapper.Map<IEnumerable<CityDto>>(results.Take(limit));
        }
    }
}