using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using HomeZone.Services.Models.LoanProperties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Services.ServiceConstants;

namespace HomeZone.Services.Implementation
{
    public class LoanPropertyService : ILoanPropertyService
    {
        private readonly HomeZoneDbContext db;

        public LoanPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<ListingPropertyServiceModel>> AllAsync(int page)
        {
            return await this.db.Properties
                .Where(p => p.IsForRent == true && p.IsSold == false)
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * PropertyListingPageSize)
                .Take(PropertyListingPageSize)
                .ProjectTo<ListingPropertyServiceModel>()
                .ToListAsync();
        }

        public async Task<PropertyDetailsServiceModel> DetailsAsync(int id)
        {
            return await this.db.Properties
                .Where(p => p.Id == id)
                .ProjectTo<PropertyDetailsServiceModel>()
                .FirstAsync();
        }

        public async Task<IEnumerable<ListingPropertyServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType, int page)
        {
            return await this.db.Properties
                .Where(p => p.IsForRent == true && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * PropertyListingPageSize)
                .Take(PropertyListingPageSize)
                .ProjectTo<ListingPropertyServiceModel>()
                .ToListAsync();
        }

        public async Task<int> TotalAsync()
        {
            return await this.db.Properties
                .Where(p => p.IsForRent == true && p.IsSold == false)
                .CountAsync();
        }

        public async Task<int> TotalSeachedAsync(int cityId, int locationId, RoomType roomType)
        {
            return await this.db.Properties
                .Where(p => p.IsForRent == true && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
                .CountAsync();
        }
    }
}
