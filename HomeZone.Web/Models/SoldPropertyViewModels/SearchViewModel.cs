using HomeZone.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeZone.Web.Models.SoldPropertyViewModels
{
    public class SearchViewModel
    {
        [Display(Name = "Град")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Квартали")]
        public int LocationId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = "Площ от:")]
        public int FromSpaceId { get; set; }

        public IEnumerable<SelectListItem> FromSpaces { get; set; }

        [Display(Name = "Площ до:")]
        public int ToSpaceId { get; set; }

        public IEnumerable<SelectListItem> ToSpaces { get; set; }

        [Display(Name = "Цена от:")]
        public int FromPriceId { get; set; }

        public IEnumerable<SelectListItem> FromPrice { get; set; }

        [Display(Name = "Цена до:")]
        public int ToPriceId { get; set; }

        public IEnumerable<SelectListItem> ToPrice { get; set; }

        [Display(Name = "Тип/Брой стаи")]
        public RoomType RoomType { get; set; }
    }
}
