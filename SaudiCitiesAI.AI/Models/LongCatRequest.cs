namespace SaudiCitiesAI.AI.Models
{
    public class LongCatRequest
    {
        public string Model { get; set; } = default!;
        public LongCatMessage[] Messages { get; set; } = default!;
        public int MaxTokens { get; set; } = 1000;
        public double Temperature { get; set; } = 0.7;
    }

    public class LongCatMessage
    {
        public string Role { get; set; } = "user";

        public string Content { get; set; } = string.Empty;
    }
}