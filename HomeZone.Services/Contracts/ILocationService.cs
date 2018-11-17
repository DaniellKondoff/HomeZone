using HomeZone.Services.Models.Location;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface ILocationService
    {
        Task<IEnumerable<ListingBasicCitiesServiceModels>> GetAllBasicCities();

        Task<IEnumerable<ListingBasicSectionServiceModel>> GetAllSectionByCity(int cityId);

        Task<IEnumerable<ListingBasicSectionServiceModel>> GetAllSectionByDefault();
    }
}
