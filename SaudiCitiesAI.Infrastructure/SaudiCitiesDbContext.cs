using Microsoft.EntityFrameworkCore;
using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Infrastructure.Persistence
{
    public class SaudiCitiesDbContext : DbContext
    {
        public SaudiCitiesDbContext(DbContextOptions<SaudiCitiesDbContext> options) : base(options) { }

        public DbSet<CityAIInsight> CityAIInsights { get; set; } = null!;
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<Attraction> Attractions { get; set; } = null!;
        public DbSet<Vision2030Focus> Vision2030Focuses { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserSearchHistory> UserSearchHistories { get; set; } = null!;
        public DbSet<UserFavorite> UserFavorites { get; set; } = null!;
        public DbSet<UserAIQuery> UserAIQueries { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // apply configurations from this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SaudiCitiesDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
