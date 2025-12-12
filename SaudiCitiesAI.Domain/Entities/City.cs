using SaudiCitiesAI.Domain.ValueObjects;

namespace SaudiCitiesAI.Domain.Entities
{
    public class City
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Region Region { get; private set; }
        public Coordinates Coordinates { get; private set; }

        private readonly List<Attraction> _attractions = new();
        public IReadOnlyCollection<Attraction> Attractions => _attractions;

        private readonly List<Vision2030Focus> _visionFocus = new();
        public IReadOnlyCollection<Vision2030Focus> VisionFocus => _visionFocus;

        public int? Population { get; set; }

        private City() { }

        public City(string name, Region region, Coordinates coordinates)
        {
            Id = Guid.NewGuid();
            Name = name;
            Region = region;
            Coordinates = coordinates;
        }

        public void AddAttraction(Attraction attraction)
        {
            _attractions.Add(attraction);
        }

        public void AddVisionFocus(Vision2030Focus focus)
        {
            _visionFocus.Add(focus);
        }
    }
}
