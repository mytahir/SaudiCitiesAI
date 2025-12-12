using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.ValueObjects;
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

            builder.Property<string>("Name")
                   .HasColumnName("Name")
                   .HasMaxLength(250)
                   .IsRequired();

            builder.Property<Guid>("CityId")
                   .HasColumnName("CityId")
                   .IsRequired();

            builder.Property<AttractionCategory>("Category")
                   .HasColumnName("Category")
                   .HasConversion<int>()
                   .IsRequired();

            // Coordinates owned
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

            builder.Property<string?>("Description")
                   .HasColumnName("Description")
                   .HasColumnType("longtext")
                   .IsRequired(false);

            builder.HasIndex("CityId").HasDatabaseName("IX_Attractions_CityId");
            builder.HasIndex("Name").HasDatabaseName("IX_Attractions_Name");
        }
    }
}