using HomeZone.Services.Admin.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static HomeZone.Web.Infrastructure.Common.WebConstants;

namespace HomeZone.Web.Areas.Admin.Controllers.WebApi
{
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly IAdminLocationService locationService;

        
        public LocationController(IAdminLocationService locationService)
        {
            this.locationService = locationService;
        }

        [HttpGet(GetLocationsByCityIdRoute)]
        public async Task<IActionResult> GetLocationsByCityId(int id)
        {
            var locations = await this.locationService.GetAllSectionByCity(id);

            if (locations == null)
            {
                return NotFound();
            }

            return this.Ok(locations);
        }
    }
}
