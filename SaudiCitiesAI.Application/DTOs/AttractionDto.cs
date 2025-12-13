namespace SaudiCitiesAI.Application.DTOs
{
    public class AttractionDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid CityId { get; set; }
    }
}