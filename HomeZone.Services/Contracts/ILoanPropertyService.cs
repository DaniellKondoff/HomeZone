using HomeZone.Data.Models;
using HomeZone.Services.Models.LoanProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface ILoanPropertyService
    {
        Task<IEnumerable<ListingPropertyServiceModel>> AllAsync(int page);

        Task<PropertyDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<ListingPropertyServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType, int page);

        Task<int> TotalAsync();

        Task<int> TotalSeachedAsync(int cityId, int locationId, RoomType roomType);
    }
}
