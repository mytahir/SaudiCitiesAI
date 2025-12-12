namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserLoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
    }
}