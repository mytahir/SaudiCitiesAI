using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Application.Services;
using SaudiCitiesAI.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<IAttractionService, AttractionService>();
builder.Services.AddScoped<IAIInsightService, AIInsightService>();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SaudiCitiesDbContext>(options =>
    options.UseMySql(connectionString,
                     new MySqlServerVersion(new Version(8, 0, 31)),
                     mysqlOptions =>
                     {
                         mysqlOptions.MigrationsAssembly(typeof(SaudiCitiesDbContext).Assembly.FullName);
                     }));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
