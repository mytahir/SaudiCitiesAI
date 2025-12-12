using System;

namespace SaudiCitiesAI.Application.DTOs
{
    public class UserActivityDto
    {
        public string ActivityType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
    }
}