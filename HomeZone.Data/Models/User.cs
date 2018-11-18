using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static HomeZone.Data.DataConstants;

namespace HomeZone.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        [MinLength(UserFirstNameMinLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        [MinLength(UserLastNameMinLength)]
        public string LastName { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
