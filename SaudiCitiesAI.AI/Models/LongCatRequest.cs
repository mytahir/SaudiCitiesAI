namespace SaudiCitiesAI.AI.Models
{
    public class LongCatRequest
    {
        public string Model { get; set; } = string.Empty;

        public LongCatMessage[] Messages { get; set; } = Array.Empty<LongCatMessage>();
    }

    public class LongCatMessage
    {
        public string Role { get; set; } = "user";

        public string Content { get; set; } = string.Empty;
    }
}