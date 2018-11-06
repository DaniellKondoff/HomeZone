using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Property;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeZone.Web.Areas.Admin.Controllers
{
    public class PropertyController : BaseAdminController
    {
        private readonly IAdminPropertyService propertyServce;

        public PropertyController(IAdminPropertyService propertyServce)
        {
            this.propertyServce = propertyServce;
        }

        public async Task<IActionResult> ListAll()
        {
            var properties = await this.propertyServce.ListAllAsync();

            var listViewModel = new PropertyListingViewModel
            {
                Properties = properties
            };

            return View(listViewModel);
        }

        public IActionResult Add()
        {
            return View();
        }
    }
}
