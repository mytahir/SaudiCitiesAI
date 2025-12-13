using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.AI.Prompts
{
    public static class Vision2030Prompt
    {
        public static string Build(City city, IEnumerable<Vision2030Focus> focus)
        {
            return $@"
Explain how the city '{city.Name}' aligns with Vision 2030 priorities.
Highlight key goals, current initiatives, and future opportunities.
Focus areas: {string.Join(", ", focus.Select(f => f.Category.ToString()))}";
        }
    }
}