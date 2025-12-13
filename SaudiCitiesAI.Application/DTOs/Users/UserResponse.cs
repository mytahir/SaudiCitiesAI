namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty; // returned once
    }
}