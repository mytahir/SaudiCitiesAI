using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace SaudiCitiesAI.Infrastructure.OSM
{
    public class OSMClient
    {
        private readonly HttpClient _http;

        public OSMClient(HttpClient http)
        {
            _http = http;
            _http.BaseAddress = new Uri("https://overpass-api.de/");
            _http.Timeout = TimeSpan.FromSeconds(90);
        }

        public async Task<JsonDocument> QueryAsync(
     string query,
     CancellationToken ct = default)
        {
            var content = new StringContent(
                query,
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            try
            {
                var response = await _http.PostAsync("api/interpreter", content, ct);
                response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync(ct);
                return await JsonDocument.ParseAsync(stream, cancellationToken: ct);
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException(
                    "OpenStreetMap service temporarily unavailable. Please retry.",
                    ex);
            }
        }
    }
}
