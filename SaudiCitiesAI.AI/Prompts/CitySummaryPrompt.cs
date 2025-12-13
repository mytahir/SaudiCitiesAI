using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.AI.Prompts
{
    public static class CitySummaryPrompt
    {
        public static string Build(City city, string mode)
        {
            return $@"
Generate an engaging {mode} summary for the Saudi city '{city.Name}'.
Include historical, cultural, economic, and notable attractions if relevant.
Make it structured, accurate, and concise.";
        }
    }
}