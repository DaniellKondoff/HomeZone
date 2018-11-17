using HomeZone.Data.Models;

namespace HomeZone.Web.Models.LoanPropertyViewModels
{
    public class SearchFormModel
    {
        public int CityId { get; set; }

        public int LocationId { get; set; }

        public RoomType RoomType { get; set; }
    }
}
