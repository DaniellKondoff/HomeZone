using HomeZone.Services.Admin.Models.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminPropertyService
    {
        Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync();
    }
}
