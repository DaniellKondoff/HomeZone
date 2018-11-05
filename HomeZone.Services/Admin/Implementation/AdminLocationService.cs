using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Location;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<AdminSectionListingServiceModel>> AllSectionAsync(int cityId)
        {
            return await this.db.Sections
                .Where(s => s.CityId == cityId)
                .ProjectTo<AdminSectionListingServiceModel>()
                .ToListAsync();
        }

        public async Task AssignedToCity(string sectioName, int cityId)
        {
            if (sectioName == null || cityId <= 0)
            {
                return;
            }

            var section = new Section
            {
                Name = sectioName,
                CityId = cityId
            };

            await this.db.Sections.AddAsync(section);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> IsAssignedToCity(int sectionId, int cityId)
        {
            return await this.db.Sections
                 .AnyAsync(s => s.CityId == cityId && s.Id == sectionId);
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

        public async Task<bool> ExistAsync(int id)
        {
            return await this.db.Cities
                .AnyAsync(c => c.Id == id);
        }

        public async Task<bool> IsAssignedToCity(string sectionName, int cityId)
        {
            return await this.db.Sections
                .AnyAsync(s => s.CityId == cityId && s.Name == sectionName);
        }

        public async Task<bool> DeleteSectionAssync(int sectionId, int cityId)
        {
            var section = this.db.Sections
                .Where(s => s.Id == sectionId && s.CityId == cityId)
                .FirstOrDefault();

            if (section == null)
            {
                return false;
            }

            this.db.Sections.Remove(section);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<string> GetCityNameAsync(int id)
        {
            var city = await this.db.Cities.FindAsync(id);

            return city?.Name;
        }
    }
}
