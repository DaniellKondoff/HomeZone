using HomeZone.Data.Models;
using HomeZone.Services.Models.SoldingProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface ISoldPropertyService
    {
        Task<IEnumerable<PropertyListingServiceModel>> AllAsync();

        Task<PropertyDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType);
    }
}
