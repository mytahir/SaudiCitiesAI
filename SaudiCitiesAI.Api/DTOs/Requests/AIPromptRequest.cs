using System;

namespace SaudiCitiesAI.Api.DTOs.Requests
{
    public class AIPromptRequest
    {
        public string CityName { get; set; } = string.Empty;
        public string Mode { get; set; } = "tourism";
        public Guid? UserId { get; set; }
    }
}