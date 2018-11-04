using HomeZone.Services.Admin.Models.Location;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Location
{
    public class LocationListingViewModel
    {
        public IEnumerable<AdminLocationListingServiceModel> AllCities { get; set; }
    }
}
