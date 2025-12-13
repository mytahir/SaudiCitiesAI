using Microsoft.Extensions.DependencyInjection;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Services;

namespace SaudiCitiesAI.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAttractionService, AttractionService>();
            services.AddScoped<IAIInsightService, AIInsightService>();

            return services;
        }
    }
}