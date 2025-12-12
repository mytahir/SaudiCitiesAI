using System;

namespace SaudiCitiesAI.Application.DTOs
{
    public class AttractionDto
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string? Description { get; set; }
    }
}