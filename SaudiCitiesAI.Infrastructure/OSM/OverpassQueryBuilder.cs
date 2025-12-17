using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Infrastructure.OSM
{
    public static class OverpassQueryBuilder
    {
        public static string BuildCitySearchQuery(string? name, int limit)
        {
            var hasName = !string.IsNullOrWhiteSpace(name);

            return hasName
                ? BuildSearchByNameQuery(name!, limit)
                : BuildGetAllQuery(limit);
        }

        private static string BuildSearchByNameQuery(string name, int limit) => $@"
[out:json][timeout:60];
area[""ISO3166-1""=""SA""]->.sa;
(
  node[""place""~""city|town""][""name""~""{name}"",i](area.sa);
  way[""place""~""city|town""][""name""~""{name}"",i](area.sa);
  relation[""place""~""city|town""][""name""~""{name}"",i](area.sa);
);
out center {limit};
";

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
    }
}
