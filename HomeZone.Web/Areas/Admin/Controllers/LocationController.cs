﻿using HomeZone.Data.Enums;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Location;
using HomeZone.Web.Infrastructure.Extensions;
using HomeZone.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static HomeZone.Web.Infrastructure.Common.WebConstants;

namespace HomeZone.Web.Areas.Admin.Controllers
{

    public class LocationController : BaseAdminController
    {
        private readonly IAdminLocationService locationService;

        public LocationController(IAdminLocationService locationService)
        {
            this.locationService = locationService;
        }

        public async Task<IActionResult> ListAll(int page = 1)
        {
            var allCities = await this.locationService.AllAsync(page);

            return View(new LocationListingViewModel
            {
                AllCities = allCities,
                CurrentPage = page,
                TotalLocations = await this.locationService.TotalAsync()
            });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Log(Operation.Add, CityTable)]
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

        [Log(Operation.Delete, CityTable)]
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

        public async Task<IActionResult> ListSections(int id)
        {
            bool isCityExist = await this.locationService.ExistAsync(id);

            if (!isCityExist)
            {
                TempData.AddErrorMessage("Invalid Request");

                return RedirectToAction(nameof(ListAll));
            }

            var list = await this.locationService.AllSectionAsync(id);
            string cityName = await this.locationService.GetCityNameAsync(id);

            return View(new SectionListingViewModel
            {
                AllSections = list,
                CityId = id,
                CityName = cityName
            });
        }

        public IActionResult CreateSection(int id)
        {
            return View(new SectionFormViewModel
            {
                CityId = id
            });
        }

        [HttpPost]
        [Log(Operation.Add, LocationTable)]
        public async Task<IActionResult> CreateSection(SectionFormViewModel model)
        {
            bool isCityExist = await this.locationService.ExistAsync(model.CityId);

            if (!isCityExist)
                ModelState.AddModelError(nameof(model.CityId), "That City does not exist.");

            bool isAssigned = await this.locationService.IsAssignedToCity(model.Name, model.CityId);
            if (isAssigned)
                ModelState.AddModelError(nameof(model.Name), "That Neighborhood is already assigned to selected City");

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await this.locationService.AssignedToCity(model.Name, model.CityId);
            TempData.AddSuccessMessage($"Neighborhood {model.Name} has been successfully saved");

            return RedirectToAction(nameof(ListSections), new { id = model.CityId });
        }

        [Log(Operation.Delete, LocationTable)]
        public async Task<IActionResult> DeleteSection(SectionDeleteViewModel model)
        {
            bool isAssigned = await this.locationService.IsAssignedToCity(model.Id, model.Cityid);

            if (!isAssigned)
            {
                ModelState.AddModelError(nameof(model.Id), "That City does not have such Neighborhood");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            bool success = await this.locationService.DeleteSectionAssync(model.Id, model.Cityid);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request");
            }
            else
            {
                TempData.AddSuccessMessage("Neighborhood has been deleted successfully");
            }

            return RedirectToAction(nameof(ListSections), new { id=model.Cityid });
        }
    }
}
