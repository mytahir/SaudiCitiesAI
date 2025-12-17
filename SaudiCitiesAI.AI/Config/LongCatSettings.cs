namespace SaudiCitiesAI.AI.Config
{
    public class LongCatSettings
    {
        public string ApiKey { get; set; } = string.Empty;
        public string BaseUrl { get; set; } = "https://api.longcat.chat/openai/v1/chat/completions";
        public string Model { get; set; } = "longcat-chat";
    }
}