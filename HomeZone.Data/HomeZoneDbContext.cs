using HomeZone.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomeZone.Data
{
    public class HomeZoneDbContext : IdentityDbContext<User>
    {
        public HomeZoneDbContext(DbContextOptions<HomeZoneDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
