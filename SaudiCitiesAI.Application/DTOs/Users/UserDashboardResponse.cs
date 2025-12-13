namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserDashboardResponse
    {
        public string Email { get; set; } = string.Empty;
        public int TotalQueries { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}