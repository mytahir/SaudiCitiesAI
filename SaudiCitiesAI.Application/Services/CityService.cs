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
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;

        public CityService(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync(
            int page = 1,
            int pageSize = 50,
            CancellationToken ct = default)
        {
            var cities = await _cityRepository.GetAllAsync(page, pageSize, ct);

            return cities.Select(MapToDto);
        }

        public async Task<CityDto?> GetByIdAsync(
            Guid id,
            CancellationToken ct = default)
        {
            var city = await _cityRepository.GetByIdAsync(id, ct);

            return city == null ? null : MapToDto(city);
        }

        public async Task<IEnumerable<CityDto>> SearchByNameAsync(
            string name,
            int limit = 50,
            CancellationToken ct = default)
        {
            var cities = await _cityRepository.SearchByNameAsync(name, limit, ct);

            return cities.Select(MapToDto);
        }

        private static CityDto MapToDto(Domain.Entities.City city)
        {
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Region = city.Region.Name,
                Latitude = city.Coordinates.Latitude,
                Longitude = city.Coordinates.Longitude
            };
        }
    }
}