using HomeZone.Data.Enums;
using System;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminLogService
    {
        Task Create(string userName, Operation operationType, string table, DateTime modifiedDate);

        Task<int> TotalAsync();

        Task ClearAsync();

        //Task<IEnumerable<LogsListingServiceModel>> AllListingAsync(int page)
    }
}
