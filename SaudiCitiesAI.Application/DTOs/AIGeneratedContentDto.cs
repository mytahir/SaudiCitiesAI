using System;

namespace SaudiCitiesAI.Application.DTOs
{
    public class AIGeneratedContentDto
    {
        public Guid CityId { get; set; }
        public string Mode { get; set; } = "tourism";
        public string Summary { get; set; } = string.Empty;
        public string RawResponse { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }
    }
}