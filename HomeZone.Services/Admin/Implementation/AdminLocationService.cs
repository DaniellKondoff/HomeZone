using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Location;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminLocationService : IAdminLocationService
    {
        private readonly HomeZoneDbContext db;

        public AdminLocationService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminLocationListingServiceModel>> AllAsync()
        {
            return await this.db.Cities
                    .ProjectTo<AdminLocationListingServiceModel>()
                    .ToListAsync();
        }

        public async Task CreateAsync(string name)
        {
            if (name == null)
            {
                return;
            }

            var city = new City
            {
                Name = name
            };

            await this.db.Cities.AddAsync(city);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var city = await this.db.Cities.FindAsync(id);

            if (city == null)
            {
                return false;
            }

            this.db.Cities.Remove(city);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await this.db.Cities
                .AnyAsync(c => c.Name == name);
        }

    }
}
