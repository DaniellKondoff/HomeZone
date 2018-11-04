using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Admin.Models.Location
{
    public class AdminLocationListingServiceModel : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
