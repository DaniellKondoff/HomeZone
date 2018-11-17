using HomeZone.Services.Contracts;
using HomeZone.Web.Infrastructure.Extensions;
using HomeZone.Web.Models.LoanPropertyViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Web.Controllers
{
    public class LoanPropertyController : Controller
    {
        private readonly ILoanPropertyService loanService;
        private readonly ILocationService locationService;
        private readonly IReservetionService reservationService;

        public LoanPropertyController(ILoanPropertyService loanService, ILocationService locationService, IReservetionService reservationService)
        {
            this.loanService = loanService;
            this.locationService = locationService;
            this.reservationService = reservationService;
        }

        public async Task<IActionResult> ListAll()
        {
            var homes = await this.loanService.AllAsync();

            return View(homes);
        }

        public async Task<IActionResult> Details(int id)
        {
            var home = await this.loanService.DetailsAsync(id);

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
                Locations = sections
            });
        }

        public async Task<IActionResult> Searched(SearchFormModel model)
        {
            var searchedHome = await this.loanService.SearchedAllAsync(model.CityId, model.LocationId, model.RoomType);

            //TODO Error Message
            if (searchedHome == null)
            {
                return BadRequest();
            }

            return View(searchedHome);
        }

        public IActionResult Reserve(int id)
        {
            return View(new ReserveViewModel
            {
                Id = id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Reserve(ReserveViewModel model)
        {
            bool isReserved = await this.reservationService.IsReservedAsync(model.Id, model.StartDate, model.EndDate);

            if (isReserved)
            {
                TempData.AddErrorMessage("There is already reservation for that time period.Please choose another dates.");
                return RedirectToAction(nameof(Reserve));
            }

            bool success = await this.reservationService.MakeReservationsAsync(model.Id, model.StartDate, model.EndDate);

            if (!success)
            {
                TempData.AddErrorMessage("Invalid Request");
            }
            else
            {
                TempData.AddSuccessMessage("Your reservation has been cofnirmed");
            }

            return RedirectToAction(nameof(ListAll));
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
    }
}
