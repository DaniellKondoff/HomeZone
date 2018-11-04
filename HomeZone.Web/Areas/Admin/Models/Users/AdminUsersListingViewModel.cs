using HomeZone.Services.Admin.Models.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace HomeZone.Web.Areas.Admin.Models.Users
{
    public class AdminUsersListingViewModel
    {
        public IEnumerable<SelectListItem> Roles { get; set; }

        public IEnumerable<AdminUserListingServiceModel> Users { get; set; }
    }
}
