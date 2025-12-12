namespace SaudiCitiesAI.Application.DTOs
{
    public class AISummaryResponse
    {
        public string Summary { get; set; } = string.Empty;
        public string PromptUsed { get; set; } = string.Empty;

        // Optional metadata from AI
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
    }
}