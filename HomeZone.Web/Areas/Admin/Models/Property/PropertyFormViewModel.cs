using HomeZone.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HomeZone.Web.Areas.Admin.Models.Property
{
    public class PropertyFormViewModel
    {
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public RoomType RoomType { get; set; }

        [Required]
        [Range(10, double.MaxValue)]
        public int Space { get; set; }

        [Required]
        [Display(Name = "City")]
        public int CityId { get; set; }

        public IEnumerable<SelectListItem> Cities { get; set; }

        [Display(Name = "Location")]
        public int? LocationId { get; set; }

        public IEnumerable<SelectListItem> Locations { get; set; }


        public bool IsForRent { get; set; }
    }
}
