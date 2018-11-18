﻿using HomeZone.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HomeZone.Data.ModelConfiguration
{
    public class ReservationCinfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasOne(r => r.User)
                 .WithMany(u => u.Reservations)
                 .HasForeignKey(r => r.UserId);
        }
    }
}
