using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.AI.Clients;
using SaudiCitiesAI.AI.Models;


namespace SaudiCitiesAI.AI.Services
{
    public class LongCatAIService : ILongCatAIService
    {
        private readonly LongCatClient _client;

        public LongCatAIService(LongCatClient client)
        {
            _client = client;
        }

        public async Task<string> GenerateAsync(
     string prompt,
     CancellationToken ct = default)
        {
            var request = new LongCatRequest
            {
                Model = "LongCat-Flash-Chat",
                Messages = new[]
                {
            new LongCatMessage
            {
                Role = "user",
                Content = prompt
            }
        },
                MaxTokens = 800,
                Temperature = 0.7
            };

            var response = await _client.SendAsync(request, ct);

            if (!response.Success)
                throw new InvalidOperationException(
                    $"LongCat AI failed: {response.Error ?? "Unknown error"}");

            return response.Content;
        }

    }
}