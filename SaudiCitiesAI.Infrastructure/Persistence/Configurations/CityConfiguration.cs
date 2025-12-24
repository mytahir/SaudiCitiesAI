using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).IsRequired();

            builder.Property(c => c.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            // Map Coordinates as owned type
            builder.OwnsOne(c => c.Coordinates, coords =>
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

            // Map Region as owned type
            builder.OwnsOne(c => c.Region, region =>
            {
                region.Property(r => r.Name)
                      .HasColumnName("RegionName")
                      .HasMaxLength(150)
                      .IsRequired(false);

                region.Property(r => r.Type)
                      .HasColumnName("RegionType")
                      .HasConversion<int>()
                      .IsRequired(false);

                // Index on RegionName
                region.HasIndex(r => r.Name).HasDatabaseName("IX_Cities_RegionName");
            });

            builder.Property(c => c.Population);

            builder.Property<string>("_knownFor")
                   .HasColumnName("KnownForJson")
                   .HasColumnType("longtext")
                   .IsRequired(false);

            // Map navigation collections via public properties
            builder.HasMany(c => c.Attractions)
                   .WithOne()
                   .HasForeignKey("CityId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.VisionFocus)
                   .WithOne()
                   .HasForeignKey("CityId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(c => c.Name).HasDatabaseName("IX_Cities_Name");
        }
    }
}