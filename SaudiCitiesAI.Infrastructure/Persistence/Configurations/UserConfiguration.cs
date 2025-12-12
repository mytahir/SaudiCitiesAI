using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("Id").IsRequired();

            builder.Property<string>("Email")
                   .HasColumnName("Email")
                   .HasMaxLength(200)
                   .IsRequired();

            // Hashed API key storage
            builder.Property<string>("ApiKeyHash")
                   .HasColumnName("ApiKeyHash")
                   .HasMaxLength(512)
                   .IsRequired();

            builder.Property<DateTime>("CreatedAt")
                   .HasColumnName("CreatedAt")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // relationships: search history, favorites, ai queries
            builder.HasMany(typeof(UserSearchHistory), "_searchHistory")
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(typeof(UserFavorite), "_favorites")
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(typeof(UserAIQuery), "_aiQueries")
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex("Email").IsUnique().HasDatabaseName("UX_Users_Email");
        }
    }
}