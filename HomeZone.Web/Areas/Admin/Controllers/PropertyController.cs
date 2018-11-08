using HomeZone.Services.Admin.Contracts;
using HomeZone.Web.Areas.Admin.Models.Property;
using HomeZone.Web.Infrastructure.Extensions;
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

        [HttpPost]
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

            if (!ModelState.IsValid)
            {
                return View(new PropertyFormViewModel
                {
                    Cities = await this.GetCitiesAsync()
                });
            }

            await this.propertyServce
                        .CreateAsync(model.Title,
                                     model.Description,
                                     model.Price,
                                     model.Space,
                                     model.RoomType,
                                     model.IsForRent,
                                     model.CityId);


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
                Space = model.Space,
                IsForRent = model.IsForRent,
                RoomType = model.RoomType,
                Cities = await this.GetCitiesAsync()
            });
        }

        [HttpPost]
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

            if (!ModelState.IsValid)
            {
                return View(new PropertyFormViewModel
                {
                    Cities = await this.GetCitiesAsync()
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
                          model.CityId);

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
