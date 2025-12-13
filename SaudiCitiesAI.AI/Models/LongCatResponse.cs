namespace SaudiCitiesAI.AI.Models
{
    public class LongCatResponse
    {
        public bool Success { get; set; }

        public string Content { get; set; } = string.Empty;

        public string? RawJson { get; set; }
    }
}