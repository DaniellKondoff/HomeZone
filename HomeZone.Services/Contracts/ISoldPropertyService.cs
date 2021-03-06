﻿using HomeZone.Data.Models;
using HomeZone.Services.Models.SoldingProperties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HomeZone.Services.Contracts
{
    public interface ISoldPropertyService
    {
        Task<IEnumerable<PropertyListingServiceModel>> AllAsync(int page);

        Task<PropertyDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<PropertyListingServiceModel>> SearchedAllAsync(int cityId, int locationId, RoomType roomType, int fromSpaceId, int toSpaceId, 
            int fromPriceId, int toPriceId, int page);

        Task<bool> IsBougthAsync(int propertyId);

        Task<bool> BuyAsync(string userId, int propertyId);

        Task<bool> ExistAsync(int id);

        Task<IEnumerable<SoldPropertyServiceModel>> MyHomesListAsync(string userId);

        Task<int> TotalAsync();
        Task<int> TotalSearchAsync(int cityId, int locationId, RoomType roomType, int fromSpaceId, int toSpaceId, int fromPriceId, int toPriceId);
    }
}
