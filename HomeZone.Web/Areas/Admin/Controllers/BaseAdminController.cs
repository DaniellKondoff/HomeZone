using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HomeZone.Web.Infrastructure.Common.WebConstants;

namespace HomeZone.Web.Areas.Admin.Controllers
{
    [Area(AdminArea)]
    [Authorize(Roles = AdministratingRole)]
    public class BaseAdminController : Controller
    {
    }
}
