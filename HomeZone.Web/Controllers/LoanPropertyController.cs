using HomeZone.Services.Contracts;
using HomeZone.Web.Models.LoanPropertyViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeZone.Web.Controllers
{
    public class LoanPropertyController : Controller
    {
        private readonly ILoanPropertyService loanService;
        private readonly ILocationService locationService;

        public LoanPropertyController(ILoanPropertyService loanService, ILocationService locationService)
        {
            this.loanService = loanService;
            this.locationService = locationService;
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
