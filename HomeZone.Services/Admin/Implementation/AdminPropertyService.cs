using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Models;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Services.ServiceConstants;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminPropertyService : IAdminPropertyService
    {
        private readonly HomeZoneDbContext db;

        public AdminPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId, int locationId, IFormFile homeImage, IFormFile homeSecondaryFile)
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
                SectionId = locationId,
                MainImage = await ConvertByteArrFromIFormFile(homeImage),
                SecondaryImage = await ConvertByteArrFromIFormFile(homeSecondaryFile)
            };

            await this.db.Properties.AddAsync(property);
            await this.db.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var property = await this.db.Properties.FindAsync(id);

            if (property == null)
            {
                return false;
            }

            this.db.Properties.Remove(property);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<AdminPropertyDetailsServiceModel> DetailsAsync(int id)
        {
            return await this.db.Properties
                .Where(p => p.Id == id)
                .ProjectTo<AdminPropertyDetailsServiceModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<bool> EditAsync(int id, string title, string description, decimal price, int space, RoomType roomType, 
            bool isForRent, int cityId, int locationId, IFormFile homeImage, IFormFile homeSecondaryImage)
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
            property.SectionId = locationId;

            if (homeImage != null)
            {
                property.MainImage = await this.ConvertByteArrFromIFormFile(homeImage);
            }

            if (homeSecondaryImage != null)
            {
                property.SecondaryImage = await this.ConvertByteArrFromIFormFile(homeSecondaryImage);
            }

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

        public async Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync(int page)
        {
            return await this.db.Properties
                 .OrderByDescending(a => a.Id)
                 .Skip((page - 1) * AdminPropertyListingPageSize)
                 .Take(AdminPropertyListingPageSize)
                 .ProjectTo<AdminPropertyListinServiceModel>()
                 .ToListAsync();
        }

        public async Task<int> TotalAsync()
        {
            return await this.db.Properties.CountAsync();
        }

        private async Task<byte[]> ConvertByteArrFromIFormFile(IFormFile file)
        {
            byte[] fileArr;

            if (file != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileArr = memoryStream.ToArray();
                }
            }
            else
            {
                fileArr = new byte[0];
            }

            return fileArr;
        } 
    }
}
