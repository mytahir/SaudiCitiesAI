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
        private readonly ICityRepository _cityRepo;

        public CityService(ICityRepository cityRepo)
        {
            _cityRepo = cityRepo;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync(int page = 1, int pageSize = 50, CancellationToken ct = default)
        {
            var cities = await _cityRepo.ListAsync(page, pageSize);
            return cities.Select(Map);
        }

        public async Task<CityDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var city = await _cityRepo.GetCityWithDetailsAsync(id, ct);
            if (city == null) return null;
            return Map(city);
        }

        public async Task<IEnumerable<CityDto>> SearchByNameAsync(string name, int limit = 50, CancellationToken ct = default)
        {
            var cities = await _cityRepo.SearchByNameAsync(name, ct);
            return cities.Take(limit).Select(Map);
        }

        private CityDto Map(Domain.Entities.City c)
        {
            return new CityDto
            {
                Id = c.Id,
                Name = c.Name,
                RegionName = c.Region?.Name ?? string.Empty,
                Latitude = Convert.ToDecimal(c.Coordinates.Latitude),
                Longitude = Convert.ToDecimal(c.Coordinates.Longitude),
                Population = c.Population,
                Attractions = c.Attractions?.Select(a => new AttractionDto
                {
                    Id = a.Id,
                    CityId = a.CityId,
                    Name = a.Name,
                    Category = a.Category.ToString(),
                    Latitude = Convert.ToDecimal(a.Coordinates.Latitude),
                    Longitude = Convert.ToDecimal(a.Coordinates.Longitude),
                    Description = a.Description
                }) ?? Array.Empty<AttractionDto>()
            };
        }
    }
}