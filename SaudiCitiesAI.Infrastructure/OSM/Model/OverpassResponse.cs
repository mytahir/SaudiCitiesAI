using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiCitiesAI.Infrastructure.OSM.Model
{
    public class OverpassResponse
    {
        public List<OverpassElement> Elements { get; set; } = new();
    }

    public class OverpassElement
    {
        public long Id { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }
        public OverpassTags? Tags { get; set; }
    }

    public class OverpassTags
    {
        public string? Name { get; set; }
        public string? NameEn { get; set; }
        public string? Population { get; set; }
        public string? Wikipedia { get; set; }
        public string? Wikidata { get; set; }
    }

}
