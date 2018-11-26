using AutoMapper.QueryableExtensions;
using HomeZone.Data;
using HomeZone.Data.Enums;
using HomeZone.Data.Models;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Services.Admin.Models.Logs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Services.ServiceConstants;

namespace HomeZone.Services.Admin.Implementation
{
    public class AdminLogService : IAdminLogService
    {
        private readonly HomeZoneDbContext db;

        public AdminLogService(HomeZoneDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<LogsListingServiceModel>> AllListingAsync(int page)
        {
            return await this.db
                .Logs
                .OrderByDescending(a => a.Id)
                .Skip((page - 1) * AdminLogsListingPageSize)
                .Take(AdminLogsListingPageSize)
                .ProjectTo<LogsListingServiceModel>()
                .ToListAsync();
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
