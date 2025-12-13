using System;

namespace SaudiCitiesAI.Api.DTOs.Requests
{
    public class AIPromptRequest
    {
        /// <summary>
        /// AI generation mode (e.g. tourism, history, business, vision2030)
        /// </summary>
        public string Mode { get; set; } = "tourism";

        /// <summary>
        /// Optional user identifier for activity tracking
        /// </summary>
        public Guid? UserId { get; set; }
    }
}