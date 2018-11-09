using HomeZone.Data.Models;
using HomeZone.Services.Admin.Models.Properties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminPropertyService
    {
        Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync();

        bool IsRoomTypeExists(int roomType);

        Task CreateAsync(string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId, int locationId);

        Task<AdminPropertyBaseServiceModel> GetByIdAsync(int id);

        Task<bool> ExistAsync(int id);

        Task<bool> EditAsync(int id, string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId, int locationId);
    }
}
