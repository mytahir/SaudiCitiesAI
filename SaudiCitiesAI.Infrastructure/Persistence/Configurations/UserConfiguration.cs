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

            // Primary key
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).HasColumnName("Id").IsRequired();

            // Email
            builder.Property(u => u.Email)
                   .HasColumnName("Email")
                   .HasMaxLength(200)
                   .IsRequired();

            // Hashed API key
            builder.Property(u => u.ApiKeyHash)
                   .HasColumnName("ApiKeyHash")
                   .HasMaxLength(512)
                   .IsRequired();

            // Total Queries
            builder.Property(u => u.TotalQueries)
                   .HasColumnName("TotalQueries")
                   .HasDefaultValue(0);

            // CreatedAt: use constructor value, remove database default
            builder.Property(u => u.CreatedAt)
                   .HasColumnName("CreatedAt")
                   .IsRequired();

            // Navigation collections
            builder.HasMany(u => u.SearchHistory)
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.Favorites)
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.AIQueries)
                   .WithOne()
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);

            // Unique index on Email
            builder.HasIndex(u => u.Email)
                   .IsUnique()
                   .HasDatabaseName("UX_Users_Email");
        }
    }
}