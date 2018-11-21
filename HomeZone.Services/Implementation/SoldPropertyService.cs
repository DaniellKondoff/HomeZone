using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using HomeZone.Services.Models.SoldingProperties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Services.Implementation
{
    public class SoldPropertyService : ISoldPropertyService
    {
        private readonly HomeZoneDbContext db;
        public SoldPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PropertyListingServiceModel>> AllAsync()
        {
            return await this.db.Properties
               .Where(p => p.IsForRent == false && p.IsSold == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();
        }

        public async Task<bool> BuyAsync(string userId, int propertyId)
        {
            bool isUserExists = await db.Users.AnyAsync(u => u.Id == userId);

            if (!isUserExists)
            {
                return false;
            }

            var property = await this.db.Properties.FindAsync(propertyId);
        
            if (property == null)
            {
                return false;
            }

            property.OwnerId = userId;
            property.IsSold = true;

            this.db.Properties.Update(property);
            await this.db.SaveChangesAsync();
            return true;
        }

        public async Task<PropertyDetailsServiceModel> DetailsAsync(int id)
        {

            return await this.db.Properties
                .Where(p => p.Id == id)
                .ProjectTo<PropertyDetailsServiceModel>()
                .FirstAsync();
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await this.db.Properties
                .AnyAsync(p => p.Id == id);
        }

        public async Task<bool> IsBougthAsync(int propertyId)
        {
            return await this.db.Properties
                .AnyAsync(p => p.Id == propertyId && p.IsSold == true);
        }

        public async Task<IEnumerable<SoldPropertyServiceModel>> MyHomesListAsync(string userId)
        {
            return await this.db.Properties
                .Where(p => p.OwnerId == userId && p.IsSold == true)
                .ProjectTo<SoldPropertyServiceModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType)
        {
            return await this.db.Properties
               .Where(p => p.IsForRent == false && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();
        }
    }
}
