using HomeZone.Data.Models;
using HomeZone.Services.Contracts;
using HomeZone.Web.Infrastructure.Common;
using HomeZone.Web.Infrastructure.Extensions;
using HomeZone.Web.Models.SoldPropertyViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Web.Controllers
{
    public class SoldPropertyController : Controller
    {
        private readonly ISoldPropertyService soldService;
        private readonly ILocationService locationService;
        private readonly IReservetionService reservationService;
        private readonly UserManager<User> userManager;

        public SoldPropertyController(
            ISoldPropertyService soldService,
            ILocationService locationService,
            IReservetionService reservationService,
             UserManager<User> userManager)
        {
            this.soldService = soldService;
            this.locationService = locationService;
            this.reservationService = reservationService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> ListAll(int page = 1)
        {
            var homes = await this.soldService.AllAsync(page);

            return View(new SoldPropertyListingModel
            {
                Properties = homes,
                CurrentPage = page,
                TotalProperties = await this.soldService.TotalAsync()
            });
        }

        public async Task<IActionResult> Details(int id)
        {
            var home = await this.soldService.DetailsAsync(id);

            if (home == null)
            {
                return BadRequest();
            }

            return View(home);
        }

        public async Task<IActionResult> Search()
        {
            var cities = await this.GetCitiesAsync();
            var sections = await this.GetSectionsAsync();

            return View(new SearchViewModel
            {
                Cities = cities,
                Locations = sections,
                FromSpaces = this.GetSpaceValues(),
                ToSpaces = this.GetSpaceValues(),
                FromPrice = this.GetPriceValues(),
                ToPrice = this.GetPriceValues()
            });
        }

        public async Task<IActionResult> Searched(SearchFormModel model, int page = 1)
        {
            var searchedHome = await this.soldService
                    .SearchedAllAsync(model.CityId, 
                                      model.LocationId, 
                                      model.RoomType,
                                      model.FromSpaceId,
                                      model.ToSpaceId,
                                      model.FromPriceId,
                                      model.ToPriceId,
                                      page);

            if (searchedHome == null)
            {
                return BadRequest();
            }

            return View(new SearchListingModel
            {
                Properties = searchedHome,
                CurrentPage = page,
                SearchModel = model,
                TotalProperties = await this.soldService
                    .TotalSearchAsync(model.CityId,
                                      model.LocationId,
                                      model.RoomType,
                                      model.FromSpaceId,
                                      model.ToSpaceId,
                                      model.FromPriceId,
                                      model.ToPriceId)
            });
        }

        [Authorize]
        public async Task<IActionResult> Buy(int id)
        {
            bool isExist = await this.soldService.ExistAsync(id);

            if (!isExist)
            {
                return BadRequest();
            }

            return View(id);
        }

        [Authorize]
        public async Task<IActionResult> FinishOrder(int id)
        {
            bool isBought = await this.soldService.IsBougthAsync(id);

            if (isBought)
            {
                TempData.AddErrorMessage("That home has been already bought.");
            }

            string userId = this.userManager.GetUserId(User);

            bool success = await this.soldService.BuyAsync(userId, id);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request.");
            }
            else
            {
                TempData.AddSuccessMessage("You have complete your order successfully");
            }

            return RedirectToAction(nameof(ListAll));
        }

        [Authorize]
        public async Task<IActionResult> MyHomes()
        {
            string userId = this.userManager.GetUserId(User);

            var myHomes = await this.soldService.MyHomesListAsync(userId);

            return View(myHomes);
        }

        private async Task<IEnumerable<SelectListItem>> GetCitiesAsync()
        {
            var cities = await this.locationService.GetAllBasicCities();

            var citiesListItems = cities
                .Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
                .ToList();

            return citiesListItems;
        }

        private async Task<IEnumerable<SelectListItem>> GetSectionsAsync()
        {
            var locations = await this.locationService.GetAllSectionByDefault();

            var locationListItems = locations
                .Select(l => new SelectListItem
                {
                    Text = l.Name,
                    Value = l.Id.ToString()
                })
                .ToList();

            return locationListItems;
        }

        private IEnumerable<SelectListItem> GetSpaceValues()
        {
            return DropDownListUtility.GetSpaceDropDown();
        }

        private IEnumerable<SelectListItem> GetPriceValues()
        {
            return DropDownListUtility.GetPriceDropDown();
        }
    }
}
