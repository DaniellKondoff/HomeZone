using HomeZone.Data;
using HomeZone.Data.Enums;
using HomeZone.Data.Models;
using HomeZone.Services.Admin.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminLogService : IAdminLogService
    {
        private readonly HomeZoneDbContext db;

        public AdminLogService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task ClearAsync()
        {
            var logs = await this.db.Logs.ToListAsync();
            this.db.Logs.RemoveRange(logs);

            await this.db.SaveChangesAsync();
        }

        public async Task Create(string userName, Operation operationType, string table, DateTime modifiedDate)
        {
            var log = new Log
            {
                UserName = userName,
                LogOperation = operationType,
                ModifiedTable = table,
                ModifiedOn = modifiedDate
            };

            this.db.Logs.Add(log);
            await this.db.SaveChangesAsync();
        }

        public async Task<int> TotalAsync()
        {
            return await this.db.Logs.CountAsync();
        }
    }
}
