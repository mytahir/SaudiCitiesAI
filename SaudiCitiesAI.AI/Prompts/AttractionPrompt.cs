using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.AI.Prompts
{
    public static class AttractionPrompt
    {
        public static string Build(Attraction attraction)
        {
            return $@"
Provide a compelling description of the attraction '{attraction.Name}' in Saudi Arabia.
Include history, significance, visitor experience, and why it's popular.";
        }
    }
}