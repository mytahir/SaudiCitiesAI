using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class UserActivityConfiguration : IEntityTypeConfiguration<UserSearchHistory>
    {
        public void Configure(EntityTypeBuilder<UserSearchHistory> builder)
        {
            builder.ToTable("UserSearchHistories");
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id).HasColumnName("Id").IsRequired();
            builder.Property<Guid>("UserId").HasColumnName("UserId").IsRequired();

            builder.Property<string>("Query")
                   .HasColumnName("Query")
                   .HasMaxLength(1000)
                   .IsRequired();

            builder.Property<DateTime>("Timestamp")
                   .HasColumnName("Timestamp")
                   .IsRequired();

            builder.HasIndex("UserId").HasDatabaseName("IX_UserSearchHistories_UserId");
        }
    }
}