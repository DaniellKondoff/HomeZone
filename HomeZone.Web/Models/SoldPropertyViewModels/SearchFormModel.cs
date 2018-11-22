using HomeZone.Data.Models;

namespace HomeZone.Web.Models.SoldPropertyViewModels
{
    public class SearchFormModel
    {
        public int CityId { get; set; }

        public int LocationId { get; set; }

        public RoomType RoomType { get; set; }

        public int FromSpaceId { get; set; }

        public int ToSpaceId { get; set; }

        public int FromPriceId { get; set; }

        public int ToPriceId { get; set; }
    }
}
