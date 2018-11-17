using HomeZone.Data.Models;
using HomeZone.Services.Models.LoanProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface ILoanPropertyService
    {
        Task<IEnumerable<ListingPropertyServiceModel>> AllAsync();

        Task<PropertyDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<ListingPropertyServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType);
    }
}
