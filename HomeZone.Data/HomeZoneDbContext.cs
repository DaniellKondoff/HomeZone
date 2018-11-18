using HomeZone.Data.ModelConfiguration;
using HomeZone.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeZone.Data
{
    public class HomeZoneDbContext : IdentityDbContext<User>
    {
        public DbSet<Property> Properties { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Section> Sections { get; set; }

        public DbSet<Reservation> Reservetions { get; set; }


        public HomeZoneDbContext(DbContextOptions<HomeZoneDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PropertyConfiguration());
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new ReservationCinfiguration());

            base.OnModelCreating(builder);
        }
    }
}
