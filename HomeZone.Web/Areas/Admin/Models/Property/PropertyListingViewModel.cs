using HomeZone.Services.Admin.Models.Properties;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Property
{
    public class PropertyListingViewModel
    {
        public IEnumerable<AdminPropertyListinServiceModel> Properties { get; set; }
    }
}
