using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

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
        }

        public async Task<JsonDocument> QueryAsync(
            string query,
            CancellationToken ct = default)
        {
            var content = new StringContent(
                query,
                Encoding.UTF8,
                "application/x-www-form-urlencoded");

            var response = await _http.PostAsync("api/interpreter", content, ct);

            // ✔ Polly already retried if needed
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync(ct);
            return await JsonDocument.ParseAsync(stream, cancellationToken: ct);
        }
    }
}
