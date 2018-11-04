using System.ComponentModel.DataAnnotations;
using static HomeZone.Data.DataConstants;

namespace HomeZone.Web.Areas.Admin.Models.Location
{
    public class LocationFormViewModel
    {
        [Required]
        [MinLength(CityNameMinLenght)]
        [MaxLength(CityNameMaxLenght)]
        public string Name { get; set; }
    }
}
