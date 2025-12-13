using AutoMapper;
using SaudiCitiesAI.Application.DTOs;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.AI.Services;
using SaudiCitiesAI.AI.Prompts;
using SaudiCitiesAI.Domain.Interfaces;

namespace SaudiCitiesAI.Application.Services
{
    public class AIInsightService : IAIInsightService
    {
        private readonly ICityRepository _cityRepository;
        private readonly LongCatAIService _aiService;
        private readonly IMapper _mapper;

        public AIInsightService(
            ICityRepository cityRepository,
            LongCatAIService aiService,
            IMapper mapper)
        {
            _cityRepository = cityRepository;
            _aiService = aiService;
            _mapper = mapper;
        }

        public async Task<AIGeneratedContentDto> GenerateCityInsightAsync(
            Guid cityId,
            string mode = "tourism",
            Guid? userId = null,
            CancellationToken ct = default)
        {
            var city = await _cityRepository.GetByIdAsync(cityId);

            if (city == null)
                throw new Exception("City not found");

            string prompt = CitySummaryPrompt.Build(city, mode);

            var aiResponse = await _aiService.GenerateAsync(prompt, userId, ct);

            return new AIGeneratedContentDto
            {
                Content = aiResponse.Content,
                RawJson = aiResponse.RawJson
            };
        }
    }
}