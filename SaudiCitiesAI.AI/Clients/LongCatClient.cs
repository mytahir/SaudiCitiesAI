using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;

namespace SaudiCitiesAI.AI.Clients
{
    public class LongCatClient
    {
        private readonly HttpClient _http;
        private readonly string _apiKey;
        private readonly ILogger<LongCatClient> _logger;

        public LongCatClient(HttpClient http, IConfiguration configuration, ILogger<LongCatClient> logger)
        {
            _http = http;
            _logger = logger;
            _apiKey = configuration.GetValue<string>("LongCatSettings:ApiKey") ?? string.Empty;
        }

        public async Task<string?> SendPromptAsync(string prompt, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(prompt)) return null;

            var payload = new
            {
                prompt = prompt,
                // you can add model, temperature, max_tokens etc as required by LongCat API
            };

            using var request = new HttpRequestMessage(HttpMethod.Post, "/v1/generate")
            {
                Content = JsonContent.Create(payload)
            };

            if (!string.IsNullOrWhiteSpace(_apiKey))
                request.Headers.Add("Authorization", $"Bearer {_apiKey}");

            HttpResponseMessage resp;
            try
            {
                resp = await _http.SendAsync(request, ct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "LongCat request failed.");
                throw;
            }

            if (!resp.IsSuccessStatusCode)
            {
                var err = await resp.Content.ReadAsStringAsync(ct);
                _logger.LogWarning("LongCat returned {Status}: {Body}", resp.StatusCode, err);
                return null;
            }

            var json = await resp.Content.ReadAsStringAsync(ct);
            // For simplicity, return raw JSON, or parse as needed.
            return json;
        }
    }
}