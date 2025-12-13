using System;

namespace SaudiCitiesAI.Api.DTOs.Responses
{
    public class AISummaryResponse
    {
        public string Content { get; set; } = string.Empty;

        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}