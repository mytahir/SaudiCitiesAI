using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;
using SaudiCitiesAI.Infrastructure.Repositories;

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

            return services;
        }
    }
}
