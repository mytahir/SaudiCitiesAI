using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.ValueObjects;
using SaudiCitiesAI.Domain.Enums;
using System.Reflection;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("Cities");

            builder.HasKey(nameof(City.Id));
            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property<string>("Name")
                .HasColumnName("Name")
                .IsRequired()
                .HasMaxLength(200);

            builder.OwnsOne(typeof(Coordinates), "_coordinates", owned =>
            {
                owned.Property<decimal>("Latitude")
                     .HasColumnName("Latitude")
                     .HasColumnType("decimal(9,6)")
                     .IsRequired();

                owned.Property<decimal>("Longitude")
                     .HasColumnName("Longitude")
                     .HasColumnType("decimal(9,6)")
                     .IsRequired();
            });

            builder.OwnsOne(typeof(Region), "_region", owned =>
            {
                owned.Property<string>("Name")
                     .HasColumnName("RegionName")
                     .HasMaxLength(150)
                     .IsRequired();

                owned.Property<RegionType>("Type")
                     .HasColumnName("RegionType")
                     .HasConversion<int>()
                     .IsRequired();
            });

            builder.Property<int?>("Population")
                   .HasColumnName("Population");

            
            builder.Property<string>("_knownFor")
                   .HasColumnName("KnownForJson")
                   .HasColumnType("longtext")
                   .IsRequired(false);

            builder.HasMany(typeof(Vision2030Focus), "_visionFocus")
                   .WithOne()
                   .HasForeignKey("CityId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(typeof(Attraction), "_attractions")
                   .WithOne()
                   .HasForeignKey("CityId")
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex("Name").HasDatabaseName("IX_Cities_Name");
            builder.HasIndex("RegionName").HasDatabaseName("IX_Cities_RegionName");
        }
    }
}