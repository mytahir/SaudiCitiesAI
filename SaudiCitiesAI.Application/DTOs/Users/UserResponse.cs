namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // This is NOT the hashed API key — only a newly generated one for the user
        public string ApiKey { get; set; } = string.Empty;
    }
}