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

        Task<bool> ExistAsync(int id);

        Task<IEnumerable<AdminSectionListingServiceModel>> AllSectionAsync(int cityId);

        Task<bool> IsAssignedToCity(string sectionName, int cityId);

        Task AssignedToCity(string sectionName, int cityId);
        Task<bool> IsAssignedToCity(int sectionId, int cityId);

        Task<bool> DeleteSectionAssync(int sectionId, int ityId);
        Task<string> GetCityNameAsync(int id);

        Task<IEnumerable<AdminLocationListingBasicServiceModel>> GetAllCitiesBasicAsync();
        Task<IEnumerable<AdminSectionListingBasicModel>> GetAllSectionByCity(int cityId);
        Task<IEnumerable<AdminSectionListingBasicModel>> GetAllSectionByFirstCity();
        Task<bool> ContainsSectionAsync(int cityId, int locationId);
    }
}
