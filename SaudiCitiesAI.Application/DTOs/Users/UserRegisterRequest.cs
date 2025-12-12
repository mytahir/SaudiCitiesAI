namespace SaudiCitiesAI.Application.DTOs.Users
{
    public class UserRegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;

        // The user’s chosen password
        public string Password { get; set; } = string.Empty;

        // Optional: initial API key if you want to allow pre-generated keys
        public string InitialApiKey { get; set; } = string.Empty;
    }
}