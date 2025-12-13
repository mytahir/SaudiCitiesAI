namespace SaudiCitiesAI.AI.Models
{
    public class LongCatRequest
    {
        public string Model { get; set; } = "longcat-chat";
        public string Prompt { get; set; } = string.Empty;
        public Guid? UserId { get; set; }
    }
}