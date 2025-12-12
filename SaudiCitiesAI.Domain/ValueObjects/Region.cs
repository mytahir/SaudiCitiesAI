using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Domain.ValueObjects
{
    public class Region
    {
        public string Name { get; }
        public RegionType Type { get; }

        private Region() { }

        public Region(string name, RegionType type)
        {
            Name = name;
            Type = type;
        }
    }
}
