﻿using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using HomeZone.Services.Models.SoldingProperties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Services.ServiceConstants;

namespace HomeZone.Services.Implementation
{
    public class SoldPropertyService : ISoldPropertyService
    {
        private readonly HomeZoneDbContext db;

        public SoldPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PropertyListingServiceModel>> AllAsync(int page)
        {
            return await this.db.Properties
               .Where(p => p.IsForRent == false && p.IsSold == false)
               .OrderByDescending(a => a.Id)
               .Skip((page - 1) * PropertyListingPageSize)
               .Take(PropertyListingPageSize)
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

        public async Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType,
            int fromSpaceId, int toSpaceId, int fromPriceId, int toPriceId)
        {
            IEnumerable<PropertyListingServiceModel> results;

            results = await db.Properties
               .Where(p => p.IsForRent == false && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();

            if (fromSpaceId == -1 && toSpaceId == -1 && fromPriceId == -1 && toPriceId == -1)
            {
                return results;
            }

            results = Filter(fromSpaceId, toSpaceId, fromPriceId, toPriceId, results);

            return results;
        }

        public async Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType, int fromSpaceId, int toSpaceId, int fromPriceId, int toPriceId, int page)
        {
            IEnumerable<PropertyListingServiceModel> results;

            results = await db.Properties
               .Where(p => p.IsForRent == false && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();

            if (fromSpaceId == -1 && toSpaceId == -1 && fromPriceId == -1 && toPriceId == -1)
            {
                return results
                    .OrderByDescending(a => a.Id)
                    .Skip((page - 1) * PropertyListingPageSize)
                    .Take(PropertyListingPageSize);
            }

            results = Filter(fromSpaceId, toSpaceId, fromPriceId, toPriceId, results);

            return results
                    .OrderByDescending(a => a.Id)
                    .Skip((page - 1) * PropertyListingPageSize)
                    .Take(PropertyListingPageSize);
        }

        private static IEnumerable<PropertyListingServiceModel> Filter(int fromSpaceId, int toSpaceId, int fromPriceId, int toPriceId, IEnumerable<PropertyListingServiceModel> results)
        {
            if (fromSpaceId != -1 && toSpaceId != -1)
            {
                results = results.Where(r => r.Space >= fromSpaceId && r.Space <= toSpaceId).ToList();
            }
            else
            {
                if (fromSpaceId != -1)
                    results = results.Where(r => r.Space >= fromSpaceId).ToList();
                else if (toSpaceId != -1)
                    results = results.Where(r => r.Space <= toSpaceId).ToList();
            }

            if (fromPriceId != -1 && toPriceId != -1)
            {
                results = results.Where(r => r.Price >= fromPriceId && r.Price <= toPriceId).ToList();
            }
            else
            {
                if (fromPriceId != -1)
                    results = results.Where(r => r.Price >= fromPriceId).ToList();
                else if (toPriceId != -1)
                    results = results.Where(r => r.Price <= toPriceId).ToList();
            }

            return results;
        }

        public async Task<int> TotalAsync()
        {
            return await this.db.Properties
                .Where(p => p.IsForRent == false && p.IsSold == false)
                .CountAsync();
        }

        public async Task<int> TotalSearchAsync(int cityId, int locationId, RoomType roomType, int fromSpaceId, int toSpaceId, int fromPriceId, int toPriceId)
        {
            IEnumerable<PropertyListingServiceModel> results;

            results = await db.Properties
               .Where(p => p.IsForRent == false && p.CityId == cityId && p.SectionId == locationId && p.RoomType == roomType && p.IsSold == false)
               .ProjectTo<PropertyListingServiceModel>()
               .ToListAsync();

            if (fromSpaceId == -1 && toSpaceId == -1 && fromPriceId == -1 && toPriceId == -1)
            {
                return results.Count();
            }

            results = Filter(fromSpaceId, toSpaceId, fromPriceId, toPriceId, results);

            return results.Count();
        }
    }
}
