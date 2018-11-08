using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Admin.Models.Properties
{
    public class AdminPropertyBaseServiceModel : IMapFrom<Property>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }

        public int Space { get; set; }

        public int CityId { get; set; }

        public bool IsForRent { get; set; }

        public int SectionId { get; set; }
    }
}
