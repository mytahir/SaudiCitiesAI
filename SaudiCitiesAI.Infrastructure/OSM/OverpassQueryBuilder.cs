using System.Text.RegularExpressions;

namespace SaudiCitiesAI.Infrastructure.OSM
{
    public static class OverpassQueryBuilder
    {
        public static string BuildCitySearchQuery(string? name, int limit)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BuildGetAllQuery(limit);

            var safeName = Regex.Escape(name);
            return BuildSearchByNameQuery(safeName, limit);
        }

        private static string BuildGetAllQuery(int limit) => $@"
[out:json][timeout:60];
area[""ISO3166-1""=""SA""]->.sa;
(
  node[""place""~""city|town""](area.sa);
  way[""place""~""city|town""](area.sa);
  relation[""place""~""city|town""](area.sa);
);
out center {limit};
";

        private static string BuildSearchByNameQuery(string name, int limit) => $@"
[out:json][timeout:60];
area[""ISO3166-1""=""SA""]->.sa;
(
  node[""place""~""city|town""][""name""~""{name}"",i](area.sa);
  node[""place""~""city|town""][""name:en""~""{name}"",i](area.sa);
  node[""place""~""city|town""][""alt_name""~""(^|;){name}($|;)"",i](area.sa);
  way[""place""~""city|town""][""name""~""{name}"",i](area.sa);
  way[""place""~""city|town""][""name:en""~""{name}"",i](area.sa);
  way[""place""~""city|town""][""alt_name""~""(^|;){name}($|;)"",i](area.sa);
  relation[""place""~""city|town""][""name""~""{name}"",i](area.sa);
  relation[""place""~""city|town""][""name:en""~""{name}"",i](area.sa);
  relation[""place""~""city|town""][""alt_name""~""(^|;){name}($|;)"",i](area.sa);
);
out center {limit};
";
    }
}