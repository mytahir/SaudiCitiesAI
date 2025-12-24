using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Interfaces;

namespace SaudiCitiesAI.Application.Services
{
    public class AIInsightService : IAIInsightService
    {
        private readonly ICityRepository _cities;
        private readonly ICityExternalProvider _externalCities;
        private readonly ILongCatAIService _ai;
        private readonly ICityRepository _cityRepository;
        private readonly ICityAIInsightRepository _insights;

        public AIInsightService(
            ICityRepository cities,
            ICityExternalProvider externalCities,
            ILongCatAIService ai,
            ICityRepository cityRepository, ICityAIInsightRepository insights)
        {
            _cities = cities;
            _externalCities = externalCities;
            _ai = ai;
            _cityRepository = cityRepository;
            _insights = insights;
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

        public async Task<AIGeneratedContentDto> GenerateCityInsightByNameAsync(
     string cityName,
     string mode,
     Guid? userId = null,
     CancellationToken ct = default)
        {
            var cities = await _cityRepository.SearchByNameAsync(cityName, 1, ct);
            var city = cities.FirstOrDefault();

            if (city == null)
                throw new InvalidOperationException("City not found in database.");

            var snapshot = new CitySnapshot(
                city.Name,
                city.Region?.Name,
                city.Coordinates.Latitude,
                city.Coordinates.Longitude,
                city.Population,
                city.Wikipedia,
                city.OsmId
            );

            return await GenerateFromSnapshot(snapshot, mode, ct);
        }
        private async Task<AIGeneratedContentDto> GenerateFromSnapshot(
            CitySnapshot city,
            string mode,
            CancellationToken ct)
        {
            // 1️⃣ Try cache / DB first
            var existing = await _insights.GetLatestAsync(city.Name, mode, ct);
            if (existing != null)
            {
                return new AIGeneratedContentDto
                {
                    Content = existing.Content
                };
            }

            // 2️⃣ Generate AI insight
            var prompt = BuildPrompt(city, mode);
            var aiResponse = await _ai.GenerateAsync(prompt, ct);

            // 3️⃣ Persist insight
            var insight = new CityAIInsight(
                cityId: null,
                cityName: city.Name,
                mode: mode,
                content: aiResponse
            );

            await _insights.AddAsync(insight, ct);

            // 4️⃣ Return
            return new AIGeneratedContentDto
            {
                Content = aiResponse
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