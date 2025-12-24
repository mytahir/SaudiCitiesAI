using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Domain.ValueObjects;

namespace SaudiCitiesAI.Application.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ICityExternalProvider _externalProvider;

        public CityService(
            ICityRepository cityRepository,
            ICityExternalProvider externalProvider)
        {
            _cityRepository = cityRepository;
            _externalProvider = externalProvider;
        }

        public async Task<IEnumerable<CityDto>> GetAllAsync(
             int page,
             int pageSize,
             CancellationToken ct = default)
        {
            var cities = await _cityRepository.ListAsync(page, pageSize, ct);

            return cities.Select(MapToDto);
        }

        // ---------------------------------------------
        // GET BY ID (DB ONLY — by design)
        // ---------------------------------------------
        public async Task<CityDto?> GetByIdAsync(
            Guid id,
            CancellationToken ct = default)
        {
            var city = await _cityRepository.GetByIdAsync(id, ct);
            return city == null ? null : MapToDto(city);
        }

        // ---------------------------------------------
        // SEARCH BY NAME (DB → OSM → CACHE)
        // ---------------------------------------------
        public async Task<IEnumerable<CityDto>> SearchByNameAsync(
            string name,
            int limit = 50,
            CancellationToken ct = default)
        {
            // 1️⃣ DB first
            var dbCities = await _cityRepository.SearchByNameAsync(name, limit, ct);
            if (dbCities.Any())
                return dbCities.Select(MapToDto);

            // 2️⃣ OSM fallback
            var osmCities = await _externalProvider.SearchAsync(name, limit, ct);
            var snapshots = osmCities.ToList();

            if (!snapshots.Any())
                return Enumerable.Empty<CityDto>();

            // 3️⃣ OPTIONAL: cache OSM results into DB
            foreach (var snap in snapshots)
            {
                var city = new City(
                    osmId: snap.OsmId,
                    name: snap.Name,
                    coordinates: new Coordinates(snap.Latitude, snap.Longitude),
                    population: snap.Population,
                    wikipedia: snap.Wikipedia,
                    wikidata: null,
                    region: null
                );

                await _cityRepository.AddAsync(city, ct);
            }

            await _cityRepository.SaveChangesAsync(ct);

            return snapshots.Select(MapFromSnapshot);
        }

        // ---------------------------------------------
        // MAPPERS
        // ---------------------------------------------
        private static CityDto MapToDto(City city)
        {
            return new CityDto
            {
                Id = city.Id,
                Name = city.Name,
                Region = city.Region?.Name,
                Latitude = city.Coordinates.Latitude,
                Longitude = city.Coordinates.Longitude
            };
        }

        private static CityDto MapFromSnapshot(CitySnapshot snapshot)
        {
            return new CityDto
            {
                Id = Guid.Empty, // not yet persisted
                Name = snapshot.Name,
                Region = snapshot.Region,
                Latitude = snapshot.Latitude,
                Longitude = snapshot.Longitude
            };
        }
    }
}
