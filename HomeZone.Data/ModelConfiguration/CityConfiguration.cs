using HomeZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace HomeZone.Data.ModelConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder
                .HasIndex(c => c.Name)
                .IsUnique();

            builder
                .HasMany(c => c.Sections)
                .WithOne(s => s.City)
                .HasForeignKey(s => s.CityId);
        }
    }
}
