using HomeZone.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HomeZone.Web.Infrastructure.Common;

namespace HomeZone.Web.Models.LoanPropertyViewModels
{
    public class SearchViewModel
    {
        [Display(Name = WebConstants.City)]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = WebConstants.Neighborhood)]
        public int LocationId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }

        [Display(Name = WebConstants.RoomType)]
        public RoomType RoomType { get; set; }
    }
}
