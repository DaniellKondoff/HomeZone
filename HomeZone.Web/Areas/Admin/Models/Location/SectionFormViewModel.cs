using System.ComponentModel.DataAnnotations;
using static HomeZone.Data.DataConstants;


namespace HomeZone.Web.Areas.Admin.Models.Location
{
    public class SectionFormViewModel
    {
        [Required]
        [MinLength(SectionNameMinLenght)]
        [MaxLength(SectionNameMaxLenght)]
        public string Name { get; set; }

        public int CityId { get; set; }
    }
}
