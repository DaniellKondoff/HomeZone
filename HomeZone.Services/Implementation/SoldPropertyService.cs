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
               .Where(p => p.IsForRent == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();
        }

        public async Task<PropertyDetailsServiceModel> DetailsAsync(int id)
        {

            return await this.db.Properties
                .Where(p => p.Id == id)
                .ProjectTo<PropertyDetailsServiceModel>()
                .FirstAsync();
        }

        public async Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType)
        {
            return await this.db.Properties
               .Where(p => p.IsForRent == false && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();
        }
    }
}
