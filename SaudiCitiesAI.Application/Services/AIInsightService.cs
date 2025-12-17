using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Interfaces;

namespace SaudiCitiesAI.Application.Services
{
    public class AIInsightService : IAIInsightService
    {
        private readonly ICityRepository _cities;
        private readonly ICityExternalProvider _externalCities;
        private readonly ILongCatAIService _ai;

        public AIInsightService(
            ICityRepository cities,
            ICityExternalProvider externalCities,
            ILongCatAIService ai)
        {
            _cities = cities;
            _externalCities = externalCities;
            _ai = ai;
        }

        public async Task<AIGeneratedContentDto> GenerateCityInsightAsync(
            Guid cityId,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default)
        {
            var city = await _cities.GetByIdAsync(cityId, ct);

            if (city == null)
                throw new InvalidOperationException(
                    "City not found in database. Use search-based insight instead.");

            var snapshot = new CitySnapshot(
                Name: city.Name,
                Region: city.Region?.Name,
                Latitude: city.Coordinates.Latitude,
                Longitude: city.Coordinates.Longitude,
                Population: city.Population,
                Wikipedia: null, // DB city has no wiki
                OsmId: city.OsmId
            );

            return await GenerateFromSnapshot(snapshot, mode, ct);
        }

        // 🔥 SEARCH-BASED INSIGHT (KEY FEATURE)
        public async Task<AIGeneratedContentDto> GenerateCityInsightByNameAsync(
            string cityName,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default)
        {
            var matches = await _externalCities.SearchAsync(cityName, 1, ct);
            var snapshot = matches.FirstOrDefault();

            if (snapshot == null)
                throw new InvalidOperationException("City not found via OpenStreetMap.");

            return await GenerateFromSnapshot(snapshot, mode, ct);
        }

        private async Task<AIGeneratedContentDto> GenerateFromSnapshot(
            CitySnapshot city,
            string mode,
            CancellationToken ct)
        {
            var prompt = BuildPrompt(city, mode);
            var aiResponse = await _ai.GenerateAsync(prompt, ct);

            return new AIGeneratedContentDto
            {
                Content = aiResponse,
                Mode = mode
            };
        }

        private static string BuildPrompt(CitySnapshot city, string mode)
        {
            return $"""
            You are an expert on Saudi Arabia cities.

            City: {city.Name}
            Region: {city.Region ?? "Unknown"}
            Coordinates: {city.Latitude}, {city.Longitude}
            Population: {city.Population?.ToString() ?? "Unknown"}
            OSM ID: {city.OsmId}
            Wikipedia: {city.Wikipedia ?? "N/A"}

            Generate a {mode}-focused insight including:
            - Cultural highlights
            - Tourism potential
            - Vision 2030 relevance
            - Key facts

            Keep it concise, informative, and professional.
            """;
        }
    }
}