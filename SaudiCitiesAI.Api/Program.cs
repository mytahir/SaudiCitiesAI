using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using SaudiCitiesAI.AI;
using SaudiCitiesAI.Application;
using SaudiCitiesAI.Application.Interfaces;
using SaudiCitiesAI.Infrastructure;
using SaudiCitiesAI.Infrastructure.BackgroundJobs.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Layer registrations
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddAI(builder.Configuration);

// ✅ Hangfire job DI registrations (ADD HERE)
builder.Services.AddScoped<ICitySyncJob, CityOsmSyncJob>();
builder.Services.AddScoped<CityOsmSyncJob>();

builder.Services.AddHangfire(config =>
{
    config.UseStorage(
        new MySqlStorage(
              builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlStorageOptions
        {
            TablesPrefix = "Hangfire",
            TransactionIsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
        }));
});

builder.Services.AddHangfireServer();

var app = builder.Build();

// Enqueue initial OSM sync job
// This runs once at startup to preload cities.
using (var scope = app.Services.CreateScope())
{
    var jobs = scope.ServiceProvider.GetRequiredService<IBackgroundJobClient>();

    jobs.Enqueue<CityOsmSyncJob>(job => job.SyncAsync(CancellationToken.None));
}

// Schedule daily OSM sync job at midnight every day to keep city data updated.
RecurringJob.AddOrUpdate<CityOsmSyncJob>(
    "sync-saudi-cities",
    job => job.SyncAsync(CancellationToken.None),
    Cron.Hourly);

RecurringJob.AddOrUpdate<ICityAIInsightJob>(
    "daily-city-ai-insights",
    job => job.RefreshDailyInsightsAsync(CancellationToken.None),
    Cron.Hourly);


app.UseHangfireDashboard("/hangfire");

// Middleware
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();