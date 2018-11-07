using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Property;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Web.Areas.Admin.Controllers
{
    public class PropertyController : BaseAdminController
    {
        private readonly IAdminPropertyService propertyServce;
        private readonly IAdminLocationService locationService;

        public PropertyController(IAdminPropertyService propertyServce, IAdminLocationService locationService)
        {
            this.propertyServce = propertyServce;
            this.locationService = locationService;
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

        public async Task<IActionResult> Add()
        {
            return View(new PropertyFormViewModel
            {
                Cities = await this.GetCitiesAsync()
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetCitiesAsync()
        {
            var cities = await this.locationService.GetAllCitiesBasicAsync();

            var citiesListItems = cities
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            return citiesListItems;
        }

    }
}
