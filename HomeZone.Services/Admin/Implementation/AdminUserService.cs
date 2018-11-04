using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Services.ServiceConstants;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminUserService : IAdminUserService
    {
        private readonly HomeZoneDbContext db;

        public AdminUserService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
        {
            return await this.db.Users
                .Where(u => u.UserName != AdministratingRole)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
        }
    }
}
