﻿using HomeZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeZone.Data.ModelConfiguration
{
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder
                .HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId);

            builder
                .HasOne(p => p.Section)
                .WithMany(s => s.Properties)
                .HasForeignKey(p => p.SectionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}