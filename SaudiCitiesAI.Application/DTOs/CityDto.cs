using System;
using System.Collections.Generic;

namespace SaudiCitiesAI.Application.DTOs
{
    public class CityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RegionName { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int? Population { get; set; }
        public IEnumerable<AttractionDto> Attractions { get; set; } = Array.Empty<AttractionDto>();
    }
}