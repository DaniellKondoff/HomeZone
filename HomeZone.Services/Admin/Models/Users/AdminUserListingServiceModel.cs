using HomeZone.Core.Mapping;
using HomeZone.Data.Models;

namespace HomeZone.Services.Admin.Models.Users
{
    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
    }
}
