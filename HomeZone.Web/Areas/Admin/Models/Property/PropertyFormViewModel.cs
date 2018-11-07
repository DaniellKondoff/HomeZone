using HomeZone.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeZone.Web.Areas.Admin.Models.Property
{
    public class PropertyFormViewModel
    {
        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public RoomType RoomType { get; set; }

        public int Space { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Required]
        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }


        public bool IsForRent { get; set; }
    }
}
