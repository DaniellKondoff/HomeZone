using System.Collections.Generic;
using System.Threading.Tasks;
using HomeZone.Data;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Properties;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminPropertyService : IAdminPropertyService
    {
        private readonly HomeZoneDbContext db;

        public AdminPropertyService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync()
        {
            return await this.db.Properties
                 .ProjectTo<AdminPropertyListinServiceModel>()
                 .ToListAsync();
        }
    }
}
