namespace SaudiCitiesAI.Domain.Entities
{
    public class CityAIInsight
    {
        private CityAIInsight() { }

        public Guid Id { get; private set; }
        public Guid? CityId { get; private set; }
        public string CityName { get; private set; } = null!;
        public string Mode { get; private set; } = null!;
        public string Content { get; private set; } = null!;
        public DateTime GeneratedAt { get; private set; }

        public CityAIInsight(
            Guid? cityId,
            string cityName,
            string mode,
            string content)
        {
            Id = Guid.NewGuid();
            CityId = cityId;
            CityName = cityName;
            Mode = mode;
            Content = content;
            GeneratedAt = DateTime.UtcNow;
        }
    }
}