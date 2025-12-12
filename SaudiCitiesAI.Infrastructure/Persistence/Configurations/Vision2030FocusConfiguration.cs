using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaudiCitiesAI.Domain.Entities;
using SaudiCitiesAI.Domain.Enums;

namespace SaudiCitiesAI.Infrastructure.Persistence.Configurations
{
    public class Vision2030FocusConfiguration : IEntityTypeConfiguration<Vision2030Focus>
    {
        public void Configure(EntityTypeBuilder<Vision2030Focus> builder)
        {
            builder.ToTable("Vision2030Focus");

            builder.HasKey(v => v.Id);

            builder.Property(v => v.Id).HasColumnName("Id").IsRequired();

            builder.Property<Guid>("CityId")
                   .HasColumnName("CityId")
                   .IsRequired();

            builder.Property<Vision2030Category>("Category")
                   .HasColumnName("Category")
                   .HasConversion<int>()
                   .IsRequired();

            builder.Property<string>("Description")
                   .HasColumnName("Description")
                   .HasColumnType("longtext")
                   .IsRequired(false);

            builder.HasIndex("CityId").HasDatabaseName("IX_Vision2030Focus_CityId");
        }
    }
}