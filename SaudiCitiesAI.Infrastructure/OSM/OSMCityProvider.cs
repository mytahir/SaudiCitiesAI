using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;

namespace SaudiCitiesAI.Infrastructure.OSM
{
    public class OSMCityProvider : ICityExternalProvider
    {
        private readonly OSMClient _client;

        public OSMCityProvider(OSMClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<CitySnapshot>> SearchAsync(
            string name,
            int limit,
            CancellationToken ct = default)
        {
            var query = OverpassQueryBuilder.BuildCitySearchQuery(name, limit);
            var json = await _client.QueryAsync(query, ct);
            return OSMCityParser.Parse(json).ToList();
        }
    }
}