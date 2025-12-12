namespace SaudiCitiesAI.Application.DTOs
{
    public class Vision2030Response
    {
        public string CityName { get; set; } = string.Empty;

        public string VisionTheme { get; set; } = string.Empty;      // e.g., "Thriving Economy"
        public string Explanation { get; set; } = string.Empty;      // High-level alignment
    }
}