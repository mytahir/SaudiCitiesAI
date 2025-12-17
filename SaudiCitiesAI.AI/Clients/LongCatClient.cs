using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SaudiCitiesAI.AI.Config;
using SaudiCitiesAI.AI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace SaudiCitiesAI.AI.Clients
{
    public class LongCatClient
    {
        private readonly HttpClient _http;

        public LongCatClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://api.longcat.chat/");
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    config["LongCat:ApiKey"]);
        }

        public async Task<LongCatResponse> SendAsync(
            LongCatRequest request,
            CancellationToken ct)
        {
            var response = await _http.PostAsJsonAsync(
                "openai/v1/chat/completions",
                request,
                ct);

            var body = await response.Content.ReadAsStringAsync(ct);

            if (!response.IsSuccessStatusCode)
            {
                return new LongCatResponse
                {
                    Success = false,
                    Error = body
                };
            }

            using var json = JsonDocument.Parse(body);

            var content =
                json.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

            return new LongCatResponse
            {
                Success = true,
                Content = content ?? string.Empty
            };
        }
    }
}