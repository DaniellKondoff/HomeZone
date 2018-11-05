using HomeZone.Services.Admin.Models.Location;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Location
{
    public class SectionListingViewModel
    {
        public IEnumerable<AdminSectionListingServiceModel> AllSections { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }
    }
}
