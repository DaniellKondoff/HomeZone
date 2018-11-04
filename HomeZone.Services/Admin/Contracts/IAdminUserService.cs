using HomeZone.Services.Admin.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
