namespace SaudiCitiesAI.Application.DTOs
{
    public class CityDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}