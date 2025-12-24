using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Application.Utils
{
    public static class RegionMapper
    {
        public static RegionType Map(string regionName)
        {
            if (string.IsNullOrWhiteSpace(regionName))
                return RegionType.Central;

            var name = regionName.ToLowerInvariant();

            // Central
            if (name.Contains("riyadh") || name.Contains("central"))
                return RegionType.Central;

            // West
            if (name.Contains("makkah") ||
                name.Contains("mecca") ||
                name.Contains("jeddah") ||
                name.Contains("taif") ||
                name.Contains("west"))
                return RegionType.West;

            // East
            if (name.Contains("eastern") ||
                name.Contains("dammam") ||
                name.Contains("khobar") ||
                name.Contains("jubail"))
                return RegionType.East;

            // South
            if (name.Contains("asir") ||
                name.Contains("jazan") ||
                name.Contains("najran") ||
                name.Contains("albaha"))
                return RegionType.South;

            // North
            if (name.Contains("tabuk") ||
                name.Contains("hail") ||
                name.Contains("jouf") ||
                name.Contains("northern"))
                return RegionType.North;

            // Safe fallback
            return RegionType.Central;
        }
    }
}