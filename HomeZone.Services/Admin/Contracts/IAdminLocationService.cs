using HomeZone.Services.Admin.Models.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminLocationService
    {
        Task<IEnumerable<AdminLocationListingServiceModel>> AllAsync();

        Task<bool> ExistAsync(string name);

        Task CreateAsync(string name);

        Task<bool> DeleteAsync(int id);
    }
}
