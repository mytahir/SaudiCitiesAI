using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class AttractionConfiguration : IEntityTypeConfiguration<Attraction>
    {
        public void Configure(EntityTypeBuilder<Attraction> builder)
        {
            builder.ToTable("Attractions");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                   .HasColumnName("Id")
                   .IsRequired();

            builder.Property(a => a.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property(a => a.CityId)
                   .HasColumnName("CityId")
                   .IsRequired();

            builder.Property(a => a.Category)
                   .HasColumnName("Category")
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property(a => a.Description)
                   .HasColumnName("Description")
                   .HasColumnType("longtext")
                   .IsRequired(false);

            // Map Coordinates as owned type
            builder.OwnsOne(a => a.Coordinates, coords =>
            {
                coords.Property(c => c.Latitude)
                      .HasColumnName("Latitude")
                      .HasColumnType("decimal(9,6)")
                      .IsRequired();

                coords.Property(c => c.Longitude)
                      .HasColumnName("Longitude")
                      .HasColumnType("decimal(9,6)")
                      .IsRequired();
            });

            // Indexes
            builder.HasIndex(a => a.CityId).HasDatabaseName("IX_Attractions_CityId");
            builder.HasIndex(a => a.Name).HasDatabaseName("IX_Attractions_Name");

            // Optional: Configure relationship to City if needed
            builder.HasOne<City>()
                   .WithMany()
                   .HasForeignKey(a => a.CityId);
        }
    }
}