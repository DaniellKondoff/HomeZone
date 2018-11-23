using HomeZone.Data.Enums;
using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Property;
using HomeZone.Web.Infrastructure.Extensions;
using HomeZone.Web.Infrastructure.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HomeZone.Web.Infrastructure.Common.WebConstants;

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
            var cities = await this.GetCitiesAsync();
            var locations = await GetLocationsAsync();

            return View(new PropertyFormViewModel
            {
                Cities = cities,
                Locations = locations
            });
        }

        [HttpPost]
        [Log(Operation.Add, PropertyTable)]
        public async Task<IActionResult> Add(PropertyFormViewModel model)
        {
            bool isRoomTypeExist = this.propertyServce.IsRoomTypeExists((int)model.RoomType);

            if (!isRoomTypeExist)
            {
                ModelState.AddModelError(nameof(model.RoomType), "Please select valid RoomType");
            }

            bool isCityExist = await this.locationService.ExistAsync(model.CityId);

            if (!isCityExist)
            {
                ModelState.AddModelError(nameof(model.CityId), "Please select a valid City");
            }

            bool isCityContainsSection = await this.locationService.ContainsSectionAsync(model.CityId, model.LocationId);

            if (!isCityContainsSection)
            {
                ModelState.AddModelError(nameof(model.LocationId), "That Location is not part of the selected city");
            }

            if (!ModelState.IsValid)
            {
                var cities = await this.GetCitiesAsync();
                var locations = await GetLocationsAsync();

                return View(new PropertyFormViewModel
                {
                    Cities = cities,
                    Locations = locations
                });
            }

            await this.propertyServce
                        .CreateAsync(model.Title,
                                     model.Description,
                                     model.Price,
                                     model.Space,
                                     model.RoomType,
                                     model.IsForRent,
                                     model.CityId,
                                     model.LocationId,
                                     model.HomeImage,
                                     model.HomeSecondaryImage);


            TempData.AddSuccessMessage($"The Home {model.Title} has been created succesfully");

            return RedirectToAction(nameof(ListAll));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await this.propertyServce.GetByIdAsync(id);

            if (model == null)
            {
                return NotFound();
            }

            return View(new PropertyFormViewModel
            {
                Title = model.Title,
                Description = model.Description,
                Price = model.Price,
                CityId = model.CityId,
                LocationId = model.SectionId,
                Space = model.Space,
                IsForRent = model.IsForRent,
                RoomType = model.RoomType,
                Cities = await this.GetCitiesAsync(),
                Locations = await GetLocationsAsyncByCityId(model.CityId)
            });
        }

        [HttpPost]
        [Log(Operation.Edit, PropertyTable)]
        public async Task<IActionResult> Edit(int id, PropertyFormViewModel model)
        {
            bool IsPropertyExist = await this.propertyServce.ExistAsync(id);

            if (!IsPropertyExist)
            {
                ModelState.AddModelError(nameof(id), "Invalid Song");
            }

            bool isRoomTypeExist = this.propertyServce.IsRoomTypeExists((int)model.RoomType);

            if (!isRoomTypeExist)
            {
                ModelState.AddModelError(nameof(model.RoomType), "Please select valid RoomType");
            }

            bool isCityExist = await this.locationService.ExistAsync(model.CityId);

            if (!isCityExist)
            {
                ModelState.AddModelError(nameof(model.CityId), "Please select a valid City");
            }

            bool isCityContainsSection = await this.locationService.ContainsSectionAsync(model.CityId, model.LocationId);

            if (!isCityContainsSection)
            {
                ModelState.AddModelError(nameof(model.LocationId), "That Location is not part of the selected city");
            }

            if (!ModelState.IsValid)
            {
                return View(new PropertyFormViewModel
                {
                    Cities = await this.GetCitiesAsync(),
                    Locations = await this.GetLocationsAsyncByCityId(model.CityId)
                });
            }


            bool success = await this.propertyServce
                .EditAsync(id,
                          model.Title,
                          model.Description,
                          model.Price,
                          model.Space,
                          model.RoomType,
                          model.IsForRent,
                          model.CityId,
                          model.LocationId,
                          model.HomeImage,
                          model.HomeSecondaryImage);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request");
            }
            else
            {
                TempData.AddSuccessMessage($" Home {model.Title} has been edited successfully");
            }

            return RedirectToAction(nameof(ListAll));
        }

        public IActionResult Delete(int id)
        {
            return View(id);
        }

        [Log(Operation.Delete, PropertyTable)]
        public async Task<IActionResult> Destroy(int id)
        {
            bool success = await this.propertyServce.DeleteAsync(id);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request");
            }
            else
            {
                TempData.AddSuccessMessage("Property has been deleted successfully");
            }

            return RedirectToAction(nameof(ListAll));
        }

        public async Task<IActionResult> Details(int id)
        {
            var property = await this.propertyServce.DetailsAsync(id);

            if (property == null)
            {
                return BadRequest();
            }
            
            return View(property);
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

        private async Task<IEnumerable<SelectListItem>> GetLocationsAsync()
        {
            var locations = await this.locationService.GetAllSectionByFirstCity();

            var locationListItems = locations
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                })
                .ToList();

            return locationListItems;
        }

        private async Task<IEnumerable<SelectListItem>> GetLocationsAsyncByCityId(int id)
        {
            var locations = await this.locationService.GetAllSectionByCity(id);

            var locationListItems = locations
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                })
                .ToList();

            return locationListItems;
        }

    }
}
