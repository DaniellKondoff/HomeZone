using HomeZone.Services;
using HomeZone.Services.Admin.Models.Location;
using System;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Location
{
    public class LocationListingViewModel
    {
        public IEnumerable<AdminLocationListingServiceModel> AllCities { get; set; }

        public int TotalLocations { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalLocations / ServiceConstants.AdminLocationListingPageSize);

        public int PreviousPage => this.CurrentPage == 1 ? 1 : this.CurrentPage - 1;

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;
    }
}
