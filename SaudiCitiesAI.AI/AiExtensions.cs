using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SaudiCitiesAI.AI.Clients;
using SaudiCitiesAI.AI.Config;
using SaudiCitiesAI.AI.Services;
using SaudiCitiesAI.Application.Interfaces;
using System;

namespace SaudiCitiesAI.AI
{
    public static class AiExtensions
    {
        public static IServiceCollection AddAI(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<LongCatSettings>(
                configuration.GetSection("LongCat"));

            services.AddHttpClient<LongCatClient>(client =>
            {
                client.BaseAddress = new Uri("https://longcat.chat/api");
            });

            services.AddScoped<ILongCatAIService, LongCatAIService>();

            return services;
        }
    }
}
