using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaudiCitiesAI.Infrastructure.Persistence;
using SaudiCitiesAI.Infrastructure.Repositories;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.AI.Clients;

namespace SaudiCitiesAI.Infrastructure
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // DbContext (MySQL via Pomelo)
            var connStr = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connStr))
                throw new InvalidOperationException("DefaultConnection is not configured.");

            services.AddDbContext<SaudiCitiesDbContext>(options =>
            {
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
                options.UseMySql(connStr, serverVersion,
                    mySqlOptions => mySqlOptions.MigrationsAssembly(typeof(SaudiCitiesDbContext).Assembly.FullName));
            });

            // Repositories
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IAttractionRepository, AttractionRepository>();

            // AI client registration (LongCat client HttpClient)
            var longcatUrl = configuration.GetValue<string>("LongCatSettings:ApiUrl") ?? string.Empty;
            services.AddHttpClient<LongCatClient>(client =>
            {
                client.BaseAddress = new Uri(longcatUrl);
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            // Other infra registrations (caching, http clients) can be added here

            return services;
        }
    }
}