using HomeZone.Data.Models;
using HomeZone.Services.Admin.Models.Properties;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Admin.Contracts
{
    public interface IAdminPropertyService
    {
        Task<IEnumerable<AdminPropertyListinServiceModel>> ListAllAsync(int page);

        bool IsRoomTypeExists(int roomType);

        Task CreateAsync(string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId, int locationId, IFormFile homeImage, IFormFile homeSecondaryImage);

        Task<AdminPropertyBaseServiceModel> GetByIdAsync(int id);

        Task<bool> ExistAsync(int id);

        Task<bool> EditAsync(int id, string title, string description, decimal price, int space, RoomType roomType, bool isForRent, int cityId, int locationId, IFormFile homeImage, IFormFile homeSecondaryImage);

        Task<bool> DeleteAsync(int id);

        Task<AdminPropertyDetailsServiceModel> DetailsAsync(int id);

        Task<int> TotalAsync();
    }
}
