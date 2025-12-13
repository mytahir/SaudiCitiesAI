using SaudiCitiesAI.AI.Clients;
using SaudiCitiesAI.AI.Models;

namespace SaudiCitiesAI.AI.Services
{
    public class LongCatAIService
    {
        private readonly LongCatClient _client;

        public LongCatAIService(LongCatClient client)
        {
            _client = client;
        }

        public async Task<LongCatResponse> GenerateAsync(
            string prompt,
            Guid? userId = null,
            CancellationToken ct = default)
        {
            var request = new LongCatRequest
            {
                Prompt = prompt,
                UserId = userId
            };

            return await _client.SendAsync(request, ct);
        }
    }
}
