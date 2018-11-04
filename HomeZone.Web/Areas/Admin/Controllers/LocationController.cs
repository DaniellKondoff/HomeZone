using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Location;
using HomeZone.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HomeZone.Web.Areas.Admin.Controllers
{

    public class LocationController : BaseAdminController
    {
        private readonly IAdminLocationService locationService;

        public LocationController(IAdminLocationService locationService)
        {
            this.locationService = locationService;
        }

        public async Task<IActionResult> ListAll()
        {
            var allCities = await this.locationService.AllAsync();

            return View(new LocationListingViewModel
            {
                AllCities = allCities
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LocationFormViewModel model)
        {
            bool exist = await this.locationService.ExistAsync(model.Name);

            if (exist)
            {
                ModelState.AddModelError(nameof(model.Name), "That City already exist.");
            }

            if (!ModelState.IsValid)
            {               
                return View(model);
            }

            await this.locationService.CreateAsync(model.Name);
            TempData.AddSuccessMessage($"City {model.Name} has been successfully saved");

            return RedirectToAction(nameof(ListAll));
        }

        public IActionResult Delete(int id)
        {
            return View(id);
        }

        public async Task<IActionResult> Destroy(int id)
        {
            bool success = await this.locationService.DeleteAsync(id);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request");
            }
            else
            {
                TempData.AddSuccessMessage("City has been deleted successfully");
            }

            return RedirectToAction(nameof(ListAll));
        }
    }
}
