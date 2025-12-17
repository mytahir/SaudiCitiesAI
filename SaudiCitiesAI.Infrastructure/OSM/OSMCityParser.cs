using SaudiCitiesAI.Application.DTOs;
using System.Text.Json;

namespace SaudiCitiesAI.Infrastructure.OSM
{
    public static class OSMCityParser
    {
        public static IEnumerable<CitySnapshot> Parse(JsonDocument doc)
        {
            if (!doc.RootElement.TryGetProperty("elements", out var elements))
                yield break;

            foreach (var el in elements.EnumerateArray())
            {
                double lat, lon;

                // node
                if (el.TryGetProperty("lat", out var latEl) &&
                    el.TryGetProperty("lon", out var lonEl))
                {
                    lat = latEl.GetDouble();
                    lon = lonEl.GetDouble();
                }
                // way / relation
                else if (el.TryGetProperty("center", out var center) &&
                         center.TryGetProperty("lat", out latEl) &&
                         center.TryGetProperty("lon", out lonEl))
                {
                    lat = latEl.GetDouble();
                    lon = lonEl.GetDouble();
                }
                else
                {
                    continue;
                }

                if (!el.TryGetProperty("tags", out var tags))
                    continue;

                var name =
                    GetString(tags, "name:en") ??
                    GetString(tags, "name") ??
                    "Unknown";

                yield return new CitySnapshot(
                    Name: name,
                    Region: GetString(tags, "is_in:state"),
                    Latitude: lat,
                    Longitude: lon,
                    Population: GetInt(tags, "population"),
                    Wikipedia: GetString(tags, "wikipedia"),
                    OsmId: el.GetProperty("id").GetInt64()
                );
            }
        }
        private static string? GetString(JsonElement tags, string key)
            => tags.TryGetProperty(key, out var v) ? v.GetString() : null;

        private static int? GetInt(JsonElement tags, string key)
        {
            if (!tags.TryGetProperty(key, out var v))
                return null;

            return int.TryParse(v.GetString(), out var i) ? i : null;
        }
    }
}
