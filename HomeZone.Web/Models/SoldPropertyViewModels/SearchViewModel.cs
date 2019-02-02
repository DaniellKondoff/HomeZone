using HomeZone.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeZone.Web.Infrastructure.Common;

namespace HomeZone.Web.Models.SoldPropertyViewModels
{
    public class SearchViewModel
    {
        [Display(Name = WebConstants.City)]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = WebConstants.Neighborhood)]
        public int LocationId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = "Space from:")]
        public int FromSpaceId { get; set; }

        public IEnumerable<SelectListItem> FromSpaces { get; set; }

        [Display(Name = "Space to:")]
        public int ToSpaceId { get; set; }

        public IEnumerable<SelectListItem> ToSpaces { get; set; }

        [Display(Name = "Price from:")]
        public int FromPriceId { get; set; }

        public IEnumerable<SelectListItem> FromPrice { get; set; }

        [Display(Name = "Price to:")]
        public int ToPriceId { get; set; }

        public IEnumerable<SelectListItem> ToPrice { get; set; }

        [Display(Name = WebConstants.RoomType)]
        public RoomType RoomType { get; set; }
    }
}
