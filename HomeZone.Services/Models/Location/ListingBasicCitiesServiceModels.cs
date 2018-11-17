using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Models.Location
{
    public class ListingBasicCitiesServiceModels : IMapFrom<City>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
