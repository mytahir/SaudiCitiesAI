using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Services;
using SaudiCitiesAI.Domain.Interfaces;
using SaudiCitiesAI.Infrastructure.Persistence;
using SaudiCitiesAI.Infrastructure.Repositories;

namespace SaudiCitiesAI.Api.Extensions
{
    public static class ApiServiceExtensions
    {
        public static IServiceCollection AddApiServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            // ---------------------------------------------
            // 1. DbContext Registration
            // ---------------------------------------------
            services.AddDbContext<SaudiCitiesDbContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("DefaultConnection"),
                    sql => sql.MigrationsAssembly("SaudiCitiesAI.Infrastructure")
                ));

            // ---------------------------------------------
            // 2. Repository Registrations (Infrastructure)
            // ---------------------------------------------
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IAttractionRepository, AttractionRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // ---------------------------------------------
            // 3. Application Services
            // ---------------------------------------------
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAttractionService, AttractionService>();
            services.AddScoped<IUserService, UserService>();

            // ---------------------------------------------
            // 4. HttpContextAccessor (needed for API key + user tracking)
            // ---------------------------------------------
            services.AddHttpContextAccessor();

            // ---------------------------------------------
            // 5. Controllers + Model Binding
            // ---------------------------------------------
            services.AddControllers()
                    .ConfigureApiBehaviorOptions(options =>
                    {
                        // Disable automatic 400 responses
                        options.SuppressModelStateInvalidFilter = true;
                    });

            // ---------------------------------------------
            // 6. Swagger (config done in ApiSwaggerExtensions)
            // ---------------------------------------------
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}