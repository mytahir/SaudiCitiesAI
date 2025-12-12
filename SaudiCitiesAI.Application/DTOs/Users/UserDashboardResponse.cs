namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserDashboardResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        public int TotalCityQueries { get; set; }
        public int TotalAIInsightsUsed { get; set; }
        public int TotalAttractionLookups { get; set; }

        public DateTime LastActive { get; set; }
    }
}