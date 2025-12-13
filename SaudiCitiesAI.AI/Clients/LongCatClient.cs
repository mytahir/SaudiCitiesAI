using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using SaudiCitiesAI.AI.Models;
using SaudiCitiesAI.AI.Config;

namespace SaudiCitiesAI.AI.Clients
{
    public class LongCatClient
    {
        private readonly HttpClient _http;
        private readonly LongCatSettings _settings;

        public LongCatClient(HttpClient http, IOptions<LongCatSettings> settings)
        {
            _http = http;
            _settings = settings.Value;

            // Configure base address + API key header
            _http.BaseAddress = new Uri(_settings.BaseUrl);
            _http.DefaultRequestHeaders.Add("x-api-key", _settings.ApiKey);
        }

        public async Task<LongCatResponse> SendAsync(LongCatRequest request, CancellationToken ct = default)
        {
            request.Model = _settings.Model;

            var response = await _http.PostAsJsonAsync(
                "/v1/chat/completions",
                request,
                ct
            );

            var json = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                return new LongCatResponse
                {
                    Success = false,
                    Content = "LongCat API request failed.",
                    RawJson = json
                };
            }

            return new LongCatResponse
            {
                Success = true,
                Content = json,
                RawJson = json
            };
        }
    }
}