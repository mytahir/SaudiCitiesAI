using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using SaudiCitiesAI.AI.Clients;
using SaudiCitiesAI.AI.Services;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Services;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.OSM;
using SaudiCitiesAI.Infrastructure.Persistence;
using SaudiCitiesAI.Infrastructure.Repositories;
using System.Net;

namespace SaudiCitiesAI.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<SaudiCitiesDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(
                        configuration.GetConnectionString("DefaultConnection")));
            });

            // Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IAttractionRepository, AttractionRepository>();
            services.AddScoped<ICityAIInsightRepository, CityAIInsightRepository>();

            // ✅ OSM HttpClient + Polly retry
            services.AddHttpClient<OSMClient>(client =>
            {
                client.BaseAddress = new Uri("https://overpass-api.de/");
                client.Timeout = TimeSpan.FromSeconds(90);
            })
            .AddPolicyHandler(GetOverpassRetryPolicy());

            services.AddScoped<ICityExternalProvider, OSMCityProvider>();

            // AI client
            services.AddHttpClient<LongCatClient>();
            services.AddScoped<ILongCatAIService, LongCatAIService>();

            services.AddScoped<IAIInsightService, AIInsightService>();


            return services;
        }

        // 🔁 Retry policy
        private static IAsyncPolicy<HttpResponseMessage> GetOverpassRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()                // 5xx, DNS, timeouts
                .OrResult(r => r.StatusCode == HttpStatusCode.TooManyRequests)
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retry =>
                        TimeSpan.FromSeconds(Math.Pow(2, retry)),
                    onRetry: (outcome, delay, retry, _) =>
                    {
                        Console.WriteLine(
                            $"[OSM] Retry {retry} after {delay.TotalSeconds}s");
                    });
        }
    }
}
