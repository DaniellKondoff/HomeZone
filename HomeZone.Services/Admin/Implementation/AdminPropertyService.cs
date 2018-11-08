using System.Collections.Generic;
using System.Threading.Tasks;
using HomeZone.Data;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Properties;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using HomeZone.Data.Models;
using System;
using System.Linq;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminPropertyService : IAdminPropertyService
    {
        private readonly HomeZoneDbContext db;

        public AdminPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId)
        {
            var property = new Property
            {
                Title = title,
                Description = description,
                Price = price,
                Space = space,
                RoomType = roomType,
                IsForRent = isForRent,
                CityId = cityId,
                SectionId = 2
            };

            await this.db.Properties.AddAsync(property);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> EditAsync(int id, string title, string description, decimal price, int space, RoomType roomType, 
            bool isForRent, int cityId)
        {
            var property = await this.db.Properties
                .FindAsync(id);

            if (property == null)
            {
                return false;
            }

            property.Title = title;
            property.Description = description;
            property.Price = price;
            property.Space = space;
            property.RoomType = roomType;
            property.IsForRent = isForRent;
            property.CityId = cityId;

            this.db.Properties.Update(property);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ExistAsync(int id)
        {
            return await this.db.Properties
                .AnyAsync(p => p.Id == id);
        }

        public async Task<AdminPropertyBaseServiceModel> GetByIdAsync(int id)
        {
            return await this.db.Properties
                .Where(p => p.Id == id)
                .ProjectTo<AdminPropertyBaseServiceModel>()
                .FirstOrDefaultAsync();
        }

        public bool IsRoomTypeExists(int roomTypeId)
        {
            var roomTypeCount = Enum.GetValues(typeof(RoomType)).Length;

            return roomTypeId >= 0 && roomTypeId < roomTypeCount;
        }

        public async Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync()
        {
            return await this.db.Properties
                 .ProjectTo<AdminPropertyListinServiceModel>()
                 .ToListAsync();
        }
    }
}
