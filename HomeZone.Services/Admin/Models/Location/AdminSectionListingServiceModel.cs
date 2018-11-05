using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Admin.Models.Location
{
    public class AdminSectionListingServiceModel : IMapFrom<Section>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }
    }
}
